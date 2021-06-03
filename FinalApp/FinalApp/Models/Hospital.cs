using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace FinalApp.Models
{
    public class Hospital
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Speciality { get; set; }

        public Bitmap Logo { get; set; }

        public List<DoctorsForUser> Doctors { get; set; }


    }
}
