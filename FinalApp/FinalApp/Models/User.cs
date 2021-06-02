using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalApp.Models
{
    public abstract class User
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        
    }
}
