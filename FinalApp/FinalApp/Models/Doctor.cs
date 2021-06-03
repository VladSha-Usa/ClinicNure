using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalApp.Models
{
    public class Doctor : User
    {
        public string Name { get; set; }
        public string Speciality { get; set; }
    }
}
