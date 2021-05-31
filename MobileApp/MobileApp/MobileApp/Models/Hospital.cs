using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MobileApp.Models
{
    public class Hospital
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Speciality { get; set; }
        public Bitmap Logo { get; set; }
        public List<DoctorForUser> Doctors { get; set; }
    }
}
