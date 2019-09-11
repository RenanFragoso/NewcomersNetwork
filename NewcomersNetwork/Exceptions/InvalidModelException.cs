using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Web;

namespace NewcomersNetworkAPI.Exceptions
{
    public class InvalidModelException : Exception
    {
        public InvalidModelException()
            :base()
        {
        }

        public InvalidModelException(string message)
            : base(message)
        {
        }

        public InvalidModelException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidModelException(IEnumerable<string> errorMessages)
            : base(string.Join(",", errorMessages.ToArray()))
        {
        }
    }
}