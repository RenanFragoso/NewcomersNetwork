using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

namespace NewcomersNetworkAPI.Providers
{
    public static class DBConn
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private static readonly int MAX_RETRY_COUNT = int.Parse(ConfigurationManager.AppSettings["SQLMaxRetryCount"]);
        private static readonly int SECONDS_BETWEEN_RETRY = int.Parse(ConfigurationManager.AppSettings["SQLSecondsBetweenRetry"]);

        public static DataSet ExecuteCommand(string sprocToExecute)
        {
            return ExecuteCommand(sprocToExecute, null);
        }

        public static SqlTransaction StartNewTransaction()
        {
            RetryPolicy retryPolicy = new RetryPolicy<SqlDatabaseTransientErrorDetectionStrategy>(MAX_RETRY_COUNT, TimeSpan.FromSeconds(SECONDS_BETWEEN_RETRY));
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlTransaction transaction = null;

            try
            {
                connection.OpenWithRetry(retryPolicy);
                transaction = connection.BeginTransaction();
            }
            catch (Exception e)
            {
                //Logger.LogError(e.Message + Environment.NewLine + e.StackTrace, "SQL transaction failed to start");
                throw;
            }

            return transaction;
        }

        public static void CommitTransaction(SqlTransaction transaction)
        {
            try
            {
                transaction.Commit();
            }
            catch (SqlException se)
            {
                transaction.Rollback();
                //Logger.LogError(se.Message + Environment.NewLine + se.StackTrace, "Sql transaction rolled-back");
                throw;
            }
        }

        public static DataSet ExecuteCommand(string sprocToExecute, Dictionary<string, object> parameters, SqlTransaction transaction = null)
        {
            RetryPolicy retryPolicy = new RetryPolicy<SqlDatabaseTransientErrorDetectionStrategy>(MAX_RETRY_COUNT, TimeSpan.FromSeconds(SECONDS_BETWEEN_RETRY));
            SqlConnection connection = null;
            DataSet dataSet = new DataSet();
            SqlDataReader reader = null;

            try
            {
                if (transaction == null)
                {
                    connection = new SqlConnection(_connectionString);
                    connection.OpenWithRetry(retryPolicy);
                }
                else
                {
                    connection = transaction.Connection;
                }

                SqlCommand command = connection.CreateCommand();
                command.CommandText = sprocToExecute;
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;

                if (parameters != null)
                {
                    foreach (string param in parameters.Keys)
                    {
                        IDbDataParameter parameter = command.CreateParameter();
                        parameter.ParameterName = param;
                        parameter.Value = parameters[param];
                        command.Parameters.Add(parameter);
                    }
                }

                reader = command.ExecuteReaderWithRetry(retryPolicy);

                do
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader, LoadOption.OverwriteChanges);
                    dataSet.Tables.Add(dataTable);
                }
                while (!reader.IsClosed);

                reader.Close();

            }
            catch (Exception e)
            {
                //Logger.LogError(e.Message + Environment.NewLine + e.StackTrace, sprocToExecute);
                throw e;
            }
            finally
            {
                if (transaction == null)
                {
                    connection.Close();
                }
            }

            return dataSet;
        }








    }
}