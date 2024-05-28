﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_api.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public Guid? OId { get; set; }
    }
}
