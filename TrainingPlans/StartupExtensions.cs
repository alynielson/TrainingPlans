using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database;
using TrainingPlans.Repositories;
using TrainingPlans.Services;

namespace TrainingPlans
{
    public static class StartupExtensions
    {
        public static void ConfigureDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<TrainingPlanDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TrainingPlansSqlDatabase")));
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPlannedRepetitionRepository, PlannedRepetitionRepository>();
            services.AddTransient<IPlannedWorkoutRepository, PlannedWorkoutRepository>();
            services.AddTransient<ICompletedWorkoutRepository, CompletedWorkoutRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IPlannedRepetitionService, PlannedRepetitionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPlannedWorkoutService, PlannedWorkoutService>();
            services.AddTransient<ICompletedWorkoutService, CompletedWorkoutService>();
        }
    }
}
