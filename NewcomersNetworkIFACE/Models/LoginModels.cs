using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewcomersNetworkIFACE.Models
{

    /// <summary>
    /// Login form ViewModel
    /// </summary>
    public class Login
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [Display(Name = "Stay Connected")]
        public bool StayConnected { get; set; } = true;

        public string ReturnUrl { get; protected set; } = "";

        public bool isValid { get; protected set; } = false;

        public bool Validate()
        {
            if(this.UserName.Length <= 0)
            {
                this.isValid = false;
                return false;
            }

            if (this.Password.Length <= 0)
            {
                this.isValid = false;
                return false;
            }

            this.isValid = true;
            return true;
        }
    }
}