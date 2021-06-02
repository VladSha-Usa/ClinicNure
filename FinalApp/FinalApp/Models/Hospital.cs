using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace FinalApp.Models
{
    public class Hospital
    {
        [Key]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Speciality { get; set; }
        public byte[] Logo { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
