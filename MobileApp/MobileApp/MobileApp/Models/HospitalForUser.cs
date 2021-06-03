using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MobileApp.Models
{
    public class HospitalForUser
    {
        public string Name { get; set; }
        public List<DoctorsForUser> Doctors { get; set; }
    }
}
