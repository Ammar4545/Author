﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Models.RequestDTO
{
    public class UserLoginTDO
    {
        [Required]
        [StringLength(150)]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Password { get; set; }
    }
}
