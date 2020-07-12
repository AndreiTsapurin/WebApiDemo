using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiDepartments.Models;

namespace WebApiDepartments
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string conDeps  = "Server=(localdb)\\mssqllocaldb;Database=departmentsdbstore;Trusted_Connection=True;";
            string conUsers = "Server=(localdb)\\mssqllocaldb;Database=usersdbstore03;Trusted_Connection=True;";

            // ������������� �������� ������
            services.AddDbContext<DepartmentsContext>(options => options.UseSqlServer(conDeps));
            services.AddDbContext<UsersContext>(options => options.UseSqlServer(conUsers));

            services.AddControllers(); // ���������� ����������� ��� �������������
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // ���������� ������������� �� �����������
            });
        }
    }
}
