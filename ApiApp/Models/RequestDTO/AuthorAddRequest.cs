using ApiApp.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Models.RequestDTO
{
    public class AuthorAddRequest
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; }
        [StringLength(30)]

        [StringArrayValidation(AllowStrings =new String[] { "wasta ","qeman"})]
        public string Location { get; set; }
               
    }
}
