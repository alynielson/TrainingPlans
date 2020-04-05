using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Database
{
    public class TrainingPlanDbContext : DbContext
    {
        public DbSet<PlannedWorkout> PlannedWorkout { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<CompletedWorkout> CompletedWorkout { get; set; }
        public DbSet<PlannedRepetition> PlannedRepetition { get; set; }
        public DbSet<CompletedRepetition> CompletedRepetition { get; set; }

        // TODO: add logging

        public TrainingPlanDbContext(DbContextOptions<TrainingPlanDbContext> ctxOptions) : base(ctxOptions)
        {
        }
    }
}
