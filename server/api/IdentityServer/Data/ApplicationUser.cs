using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public Guid? OId { get; set; }
    }
}
