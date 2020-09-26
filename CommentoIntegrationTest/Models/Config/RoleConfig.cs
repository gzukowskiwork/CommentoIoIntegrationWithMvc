using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentoIntegrationTest.Models.Config
{
    public class RoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
           new IdentityRole
           {
               Name = "RegisteredUser",
               NormalizedName = "REGISTEREDUSER"
           });
        }
    }
}