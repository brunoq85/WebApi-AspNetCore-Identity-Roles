using Dev.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dev.Api.Mapping
{
    public class AspNetClaimMapping : IEntityTypeConfiguration<AspNetClaim>
    {
        public void Configure(EntityTypeBuilder<AspNetClaim> builder)
        {
            builder.ToTable("AspNetClaims");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();
            builder.Property(c=>c.ClaimType).IsRequired().HasMaxLength(100);
            builder.Property(c => c.ClaimValue).IsRequired().HasMaxLength(100);
        }
    }
}
