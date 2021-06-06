using Dev.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dev.Api.Mapping
{
    public class AspNetRoleClaimMapping : IEntityTypeConfiguration<AspNetRoleClaim>
    {
        public void Configure(EntityTypeBuilder<AspNetRoleClaim> builder)
        {
            builder.ToTable("AspNetRoleClaims");

            builder.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            builder.Property(e => e.RoleId).IsRequired();

            builder.HasKey(table => new
            {
                table.RoleId,
                table.ClaimId
            });

            builder.HasOne(rc => rc.Claim)
                .WithMany(r => r.AspNetRoleClaims)
                .HasForeignKey(rc => rc.ClaimId);

            builder.Ignore(rc => rc.ClaimType);
            builder.Ignore(rc => rc.ClaimValue);
        }
    }
}
