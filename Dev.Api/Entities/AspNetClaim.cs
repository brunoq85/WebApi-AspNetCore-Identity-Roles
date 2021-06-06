using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;

namespace Dev.Api.Entities
{
    public class AspNetClaim   : Notifiable
    {
        public AspNetClaim(string claimType, string claimValue)
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaim>();

            AddNotifications(new Contract()
                  .Requires()
                  .IsNotNullOrEmpty(ClaimType, "AspNetClaim.ClaimType", "O campo tipo deve ser preenchido")
                  .HasMinLen(ClaimType, 3, "AspNetClaim.ClaimType", "O campo tipo deve ser no mínimo 3 caracteres")
                  .HasMaxLen(ClaimType, 100, "AspNetClaim.ClaimType", "O campo tipo deve ser no máximo 100 caracteres")
                  .IsNotNullOrEmpty(ClaimValue, "AspNetClaim.ClaimValue", "O campo valor deve ser preenchido")
                  .HasMinLen(ClaimValue, 3, "AspNetClaim.ClaimValue", "O valor valor deve ser no mínimo 3 caracteres")
                  .HasMaxLen(ClaimValue, 100, "AspNetClaim.ClaimValue", "O campo valor deve ser no máximo 100 caracteres")
                );
        }

        public string Id { get; set; }
        public string ClaimType { get; private set; }
        public string ClaimValue { get; private set; }

        public ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }          
    }
}
