using CommentoIntegrationTest.Models.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommentoIntegrationTest.Models
{
    public class PeopleContext : IdentityDbContext<ApplicationUser>
    {
        public PeopleContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PeopleConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
        }

        public DbSet<People> Peoples { get; set; }

    }
}
