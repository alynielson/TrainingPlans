using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrainingPlans.ExceptionHandling;
using Microsoft.Extensions.Logging;
using TrainingPlans.Database;
using TrainingPlans.Database.Models;
using TrainingPlans.Repositories;
using TrainingPlans.Services;
using TrainingPlans.ViewModels.Validators;
using TrainingPlans.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
namespace TrainingPlans
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabaseServices(Configuration);

            services.AddTransient<ICustomValidator<PlannedWorkoutVM>, PlannedWorkoutVMValidator>();
            services.InjectRepositories();
            services.InjectServices();
            services.AddControllers(options =>
            {
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CustomExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();

            //app.UseAuthorization();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
