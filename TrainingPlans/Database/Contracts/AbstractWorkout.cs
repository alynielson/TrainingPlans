using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Database.Contracts
{
    public abstract class AbstractWorkout : IIdentifiable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ActivityType ActivityType { get; set; }
        public WorkoutType WorkoutType { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; } // This should be required, but making it nullable to not cause a bunch of sql cascade problems on deleting user.

        public AbstractWorkout(AbstractWorkoutVM viewModel, int userId, int id)
        {
            Id = id;
            Name = viewModel.Name;
            ActivityType = viewModel.ActivityType;
            WorkoutType = viewModel.WorkoutType;
            UserId = userId;
        }

        public AbstractWorkout() { }

        protected List<T> GetRepetitionsFromVM<T, Tvm>(List<T> newReps, List<T> existingReps, List<Tvm> viewModelReps) 
            where T : AbstractRepetition where Tvm : AbstractRepetitionVM
        {
            var oldRepVMs = viewModelReps?.Where(x => x.Id != 0).ToDictionary(x => x.Id, x => x);
            var oldReps = existingReps.Select(x =>
            {
                if (oldRepVMs.TryGetValue(x.Id, out var vm))
                {
                    x.UpdateFromWorkoutVM(vm, ActivityType);
                    return x;
                }
                return null;
            }).Where(x => x != null).ToList();
            newReps.AddRange(oldReps);
            return newReps;
        }

        protected virtual void UpdateFromVM(AbstractWorkoutVM viewModel)
        {
            Name = viewModel.Name;
            ActivityType = viewModel.ActivityType;
            WorkoutType = viewModel.WorkoutType;
        }
    }
}
