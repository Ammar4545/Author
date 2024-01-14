using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Models.RequestDTO
{
    public class UserAddTDO
    {
        [Required]
        [StringLength(150)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        public string Password { get; set; }
    }
}
