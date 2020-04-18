using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Contracts;
using TrainingPlans.Database.Models;

namespace TrainingPlans.ViewModels
{
    public abstract class AbstractWorkoutVM: IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ActivityType ActivityType { get; set; }
        public WorkoutType WorkoutType { get; set; }

        public AbstractWorkoutVM(AbstractWorkout model)
        {
            Id = model.Id;
            Name = model.Name;
            ActivityType = model.ActivityType;
            WorkoutType = model.WorkoutType;
        }
        public AbstractWorkoutVM() { }
    }
}
