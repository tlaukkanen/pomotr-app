using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PomotrApp.Models
{
    public class FamilyMember : IdentityUser
    {
        public Family Family { get; set; }
    }
}
