using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Models.Entities
{
    public class Users
    {
        [Key]
        [Required]
        [StringLength(150)]
        public string UserId { get; set; }
        [Required]
        [StringLength(150)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }
        [Required]
        [StringLength(250)]
        public byte[] PasswordHash { get; set; }
        [Required]
        [StringLength(250)]
        public byte[] PasswordSalt { get; set; }
    }
}
