using AutoMapper;
using CommentoIntegrationTest.Middleware;
using CommentoIntegrationTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CommentoIntegrationTest.Extensions
{
    public static class ServiceExtensions
    {
        public static void ServiceInstaller(this IServiceCollection services, IConfiguration Configuration)
        {
            string connectionString = Configuration["ConnectionString:sqlConnection"];

            //Connection string was hardcoded due to identity not updating database
            services.AddDbContext<PeopleContext>(options =>
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Commento;Trusted_Connection=True"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<PeopleContext>();

            services.AddControllersWithViews();

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ICreateUrlForSSO, CreateUrlForSSO>();

            services.AddDistributedMemoryCache();
            services.AddSession();
        }
    }
}
