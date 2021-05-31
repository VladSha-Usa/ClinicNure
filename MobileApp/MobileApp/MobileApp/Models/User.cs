using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public abstract class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
