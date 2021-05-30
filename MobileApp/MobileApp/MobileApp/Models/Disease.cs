using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Disease
    {
        public string Name { get; set; }
        public Symptom[] Symptoms { get; set; }
    }
}
