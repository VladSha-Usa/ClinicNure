﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public List<Symptom> Symptoms { get; set; }
        public Patient Patient { get; set; }
        public HospitalForUser Hospital { get; set; }
        public DoctorsForUser Doctor { get; set; }
        public string Status { get; set; }
        public Disease Disease { get; set; }
    }
}
