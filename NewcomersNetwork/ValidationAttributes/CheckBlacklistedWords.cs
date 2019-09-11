using NewcomersNetworkAPI.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewcomersNetworkAPI.ValidationAttributes
{
    public class CheckBlacklistedWords : ValidationAttribute
    {
        protected override ValidationResult IsValid(object text, ValidationContext validationContext)
        {
            if(BlackListWordChecker.Instance.IsInappropriate(text.ToString()))
            {
                return new ValidationResult("Text contains inapropriate words.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}