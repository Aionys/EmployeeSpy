using EmployeeSpy.DataAccessEF;
using EmployeeSpy.Models;
using EmployeeSpy.Abstractions;
using EmployeeSpy.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.Extensions.Logging;

namespace EmployeeSpy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var config = GetConfig();

            services.AddDbContextPool<EmployeeSpyContext>(options =>
              options
                  .UseSqlServer(config.EmployeeSpyDatabase));

            services.AddScoped<IRepository<Room>, RepositoryBase<Room>>();
            services.AddScoped<IRepository<Door>, RepositoryBase<Door>>();
            services.AddScoped<IRepository<Visitor>, RepositoryBase<Visitor>>();
            services.AddScoped<IRepository<Employee>, RepositoryBase<Employee>>();

            services.AddControllers();
        }

        private EmployeeSpyConfig GetConfig()
        {
            var config = new EmployeeSpyConfig();
            Configuration.GetSection("EmployeeSpy").Bind(config);

            return config;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EmployeeSpyContext context, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            DbInitializer.Initialize(context);

            app.UseRouting();
            app.UseAuthorization();

            app.UseLoggingMiddleware(loggerFactory);
            app.UseExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
