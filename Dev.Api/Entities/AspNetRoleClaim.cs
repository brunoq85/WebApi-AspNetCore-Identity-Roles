using Microsoft.AspNetCore.Identity;
using System;

namespace Dev.Api.Entities
{
    public class AspNetRoleClaim : IdentityRoleClaim<string>
    {
        //public Guid RoleId { get; set; }
        public string ClaimId { get; set; }

        public virtual AspNetClaim Claim { get; set; }
    }
}
