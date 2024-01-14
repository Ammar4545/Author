using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.CustomValidation
{
    public class StringArrayValidation:ValidationAttribute
    {

        public String [] AllowStrings { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //if (AllowStrings.Contains(value))
            //{
                return ValidationResult.Success;
            //}
            //return new ValidationResult("the value is not allowed");
        }
    }
}
