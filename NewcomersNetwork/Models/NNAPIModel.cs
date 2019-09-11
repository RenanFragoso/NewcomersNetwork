using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Threading.Tasks;
using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NewcomersNetworkAPI.Exceptions;

namespace NewcomersNetworkAPI.Models
{
    public abstract class NNAPIModel
    {

        [JsonIgnore]
        public bool isValid { get; protected set; } = false;
        [JsonIgnore]
        public List<string> sMsgError { get; set; } = new List<string>();

        public virtual void MapFromTableRow(DataRow row)
        {
        }
        public virtual bool Validate()
        {
            return false;
        }
        public virtual void ValidOrBreak()
        {
            throw new InvalidModelException("Invalid Model");
        }
        public virtual bool Delete()
        {
            return false;
        }
        public virtual bool Save()
        {
            return false;
        }
        public virtual bool Update()
        {
            return false;
        }

        public virtual async Task<bool> SaveAsync()
        {
            return false;
        }

        public virtual async Task<bool> UpdateAsync()
        {
            return false;
        }

        public virtual async Task<bool> DeleteAsync()
        {
            return false;
        }

        public virtual string GetErrors()
        {
            return string.Join(",",this.sMsgError.ToArray());
        }

        public virtual void LoadFullDetails()
        {
        }

    }
}