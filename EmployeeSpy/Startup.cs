using EmployeeSpy.Core.Abstractions;
using EmployeeSpy.Configurations;
using EmployeeSpy.DataAccessEF;
using EmployeeSpy.Extensions;
using EmployeeSpy.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EmployeeSpy.Services;
using EmployeeSpy.DataAccessEF.Repositories;

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

            services.AddScoped<IRoomsRepository, RoomsRepository>();
            services.AddScoped<IRepository<Door>, RepositoryBase<Door>>();
            services.AddScoped<IRepository<Visitor>, RepositoryBase<Visitor>>();
            services.AddScoped<IRepository<Employee>, RepositoryBase<Employee>>();
            services.AddScoped<IRepository<MovementLogRecord>, RepositoryBase<MovementLogRecord>>();

            services.AddScoped<IGateKeeperService, GateKeeperService>();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer", options =>
                {
                    options.ApiName = config.Identity.ApiName;
                    options.RequireHttpsMetadata = config.Identity.RequireHttpsMetadata;
                    options.Authority = config.Identity.Authority;
                });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EmployeeSpyContext context, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            DbInitializer.Initialize(context);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseLoggingMiddleware(loggerFactory);
            app.UseExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private EmployeeSpyConfig GetConfig()
        {
            var config = new EmployeeSpyConfig();
            Configuration.GetSection("EmployeeSpy").Bind(config);

            return config;
        }
    }
}
