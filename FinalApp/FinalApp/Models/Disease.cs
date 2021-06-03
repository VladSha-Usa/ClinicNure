using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalApp.Models
{
    public class Disease
    {
        [Key]
        public string Name { get; set; }
        public List<Symptom> Symptoms { get; set; }
    }
}
