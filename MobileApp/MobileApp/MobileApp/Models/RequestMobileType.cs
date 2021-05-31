using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class RequestMobileType
    {
        public string Date { get; set; }
        public string Symptoms { get; set; }
        public Patient Patient { get; set; }
        public Hospital Hospital { get; set; }
        public DoctorForUser Doctor { get; set; }
        public string State { get; set; }
        public Disease Disease { get; set; }
    }
}
