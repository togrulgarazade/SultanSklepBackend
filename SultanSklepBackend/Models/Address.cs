﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SultanSklepBackend.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string MNumber { get; set; }
        public string Flat { get; set; }
        public string Post { get; set; }
        public string Not { get; set; }

    }
}
