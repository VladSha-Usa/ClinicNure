﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalApp.Models
{
    public class HospitalAdmin : User
    {
        public Hospital Hospital { get; set; }
    }
}
