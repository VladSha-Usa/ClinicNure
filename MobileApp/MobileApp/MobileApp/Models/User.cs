using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        // Maybe usefull, idk
        public override bool Equals(object obj)
        {
            User user = obj as User;
            return this.Email.Equals(user.Email) && this.Password.Equals(user.Password);
        }
    }
}
