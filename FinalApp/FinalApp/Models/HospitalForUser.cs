using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalApp.Models
{
    public class HospitalForUser
    {
        public string Name { get; set; }
        public List<DoctorsForUser> Doctors { get; set; }
    }
}
