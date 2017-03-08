using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewcomersNetworkIFACE.Models
{
    public class SignIn
    {

        public bool isValid { get; protected set; } = false;

        public SignIn()
        {
        }

        public bool Validate()
        {
            this.isValid = true;
            return true;
        }

    }
}