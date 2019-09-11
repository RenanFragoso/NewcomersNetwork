using NewcomersNetworkAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NewcomersNetworkAPI.Providers
{
    public sealed class BlackListWordChecker : NNAPIModel
    {

        public List<string> BlackListedWords { get; protected set; } = new List<string>();
        private static volatile BlackListWordChecker instance;
        private static object syncRoot = new Object();

        public static BlackListWordChecker Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new BlackListWordChecker();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Initializes the singleton
        /// </summary>
        public static void Init()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new BlackListWordChecker();
                        instance.LoadWordList();
                    }
                }
            }
        }

        public BlackListWordChecker()
        {
        }

        /// <summary>
        /// Loads the Black Listed Words from the database
        /// and adds into the list for future comparisons
        /// </summary>
        public void LoadWordList()
        {
            DataTable oWordData = DBConn.ExecuteCommand("sp_BlackListWords_GetActive", null).Tables[0];
            if (!oWordData.HasErrors)
            {
                foreach(DataRow oRow in oWordData.Rows)
                {
                    this.BlackListedWords.Add(oRow["Word"].ToString());
                }
            }
        }

        /// <summary>
        /// Reloads the Black Listed Words list
        /// </summary>
        public void ReloadWordList()
        {
            this.BlackListedWords.Clear();
            this.LoadWordList();
        }

        /// <summary>
        /// Tests for inappropriate text
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool IsInappropriate(string cText)
        {
            string normalizedContent = cText.Replace("-", string.Empty).Replace(".", string.Empty).Replace(",", string.Empty).Replace("|", string.Empty).Replace("/", string.Empty).Replace(";", string.Empty).ToLower() + " ";
            return this.BlackListedWords.Any(wrd => normalizedContent.Contains(wrd + " "));
        }

        /// <summary>
        /// Adds a new word to the dictionary
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool AddWord(string word)
        {
            if (word != null && word.Length > 0 && !this.IsInappropriate(word))
            {
                Dictionary<string, object> infoParameters = new Dictionary<string, object>();
                infoParameters.Add("cWord", word);
                try
                {
                    DataTable oWordData = DBConn.ExecuteCommand("sp_BlackListWords_AddWord", infoParameters).Tables[0];
                    if (!oWordData.HasErrors)
                    {
                        this.ReloadWordList();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception error)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool DeleteWord(string word)
        {
            if (word != null && word.Length > 0)
            {
                Dictionary<string, object> infoParameters = new Dictionary<string, object>();
                infoParameters.Add("cWord", word);
                try
                {
                    DataTable oWordData = DBConn.ExecuteCommand("sp_BlackListWords_DeleteWord", infoParameters).Tables[0];
                    if (!oWordData.HasErrors)
                    {
                        this.ReloadWordList();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception error)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}