using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApiUsers.Models;

namespace WebApiUsers
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string conDeps = "Server=(localdb)\\mssqllocaldb;Database=departmentsdbstore;Trusted_Connection=True;";
            string conUsers = "Server=(localdb)\\mssqllocaldb;Database=usersdbstore05;Trusted_Connection=True;";

            // Setup data contexts
            services.AddDbContext<DepartmentsContext>(options => options.UseSqlServer(conDeps));
            services.AddDbContext<UsersContext>(options => options.UseSqlServer(conUsers));

            services.AddControllers(); // use controllers without views
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // set routing on the controllers
            });
        }
    }
}
