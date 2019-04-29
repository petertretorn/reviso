using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Reviso.Application;
using Reviso.Data;
using Reviso.Domain.Interfaces;
using Reviso.Domain.Services;

namespace Reviso.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<DbContext, RevisoContext>();
            services.AddScoped<IRegistrationService, RegistrationService>(); 
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ICalculateService, CalculateService>();
            services.AddScoped<ICalculateStrategy, CalculateStrategy>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOriginsHeadersAndMethods",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAllOriginsHeadersAndMethods");

            app.UseMvc();
        }
    }
}
