﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { set; get; }
    }
}
