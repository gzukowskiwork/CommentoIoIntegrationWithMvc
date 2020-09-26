﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentoIntegrationTest.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public int Age { get; set; }
    }
}
