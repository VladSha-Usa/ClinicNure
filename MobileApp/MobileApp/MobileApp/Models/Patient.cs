using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Patient : User
    {
        public string Name { get; set; }
        public bool IsRegistration { get; set; }
    }
}
