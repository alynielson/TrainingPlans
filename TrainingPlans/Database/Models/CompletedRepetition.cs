using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Contracts;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Database.Models
{
    public class CompletedRepetition : AbstractRepetition
    {
        public PlannedRepetition PlannedRepetition { get; set; }
        [ForeignKey("PlannedRepetition")]
        public int? PlannedRepetitionId { get; set; }
        public CompletedWorkout CompletedWorkout { get; set; }
        [ForeignKey("CompletedWorkout")]
        public int CompletedWorkoutId { get; set; }

        public CompletedRepetition(CompletedRepetitionVM viewModel, ActivityType activityType, int userId, int id, int workoutId) : base(viewModel, activityType, userId, id)
        {
            PlannedRepetitionId = viewModel.PlannedRepetitionId;
            CompletedWorkoutId = workoutId;
        }

        public void UpdateFromWorkoutVM(CompletedRepetitionVM viewModel, ActivityType activityType)
        {
            base.UpdateFromWorkoutVM(viewModel, activityType);
            PlannedRepetitionId = viewModel.PlannedRepetitionId;
        }

        public CompletedRepetition() : base() { }
    }
}
