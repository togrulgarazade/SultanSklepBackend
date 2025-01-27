﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SultanSklepBackend.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
