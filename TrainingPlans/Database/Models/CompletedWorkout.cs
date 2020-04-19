using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Contracts;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Database.Models
{
    public class CompletedWorkout : AbstractWorkout
    {
        public DateTimeOffset CompletedDateTime { get; set; }
        public PlannedWorkout PlannedWorkout { get; set; }
        [ForeignKey("PlannedWorkout")]
        public int? PlannedWorkoutId { get; set; }
        public List<CompletedRepetition> CompletedRepetitions { get; set; }

        public CompletedWorkout() : base() { }
        public CompletedWorkout(CompletedWorkoutVM viewModel, int userId, int id) : base(viewModel, userId, id)
        {
            CompletedRepetitions = viewModel.CompletedRepetitions?.Select(x => new CompletedRepetition(x, ActivityType, userId, x.Id, id)).ToList();
            CompletedDateTime = DateTime.Parse(viewModel.CompletedDateTime);
            PlannedWorkoutId = viewModel.PlannedWorkoutId;
        }

        public void UpdateFromVM(CompletedWorkoutVM viewModel)
        {
            base.UpdateFromVM(viewModel);
            CompletedRepetitions = GetCompletedRepetitionsFromVM(viewModel.CompletedRepetitions);
            CompletedDateTime = DateTime.Parse(viewModel.CompletedDateTime);
            PlannedWorkoutId = viewModel.PlannedWorkoutId;
        }

        private List<CompletedRepetition> GetCompletedRepetitionsFromVM(List<CompletedRepetitionVM> viewModelReps)
        {
            var newReps = viewModelReps?.Where(x => x.Id == 0).Select(x => new CompletedRepetition(x, ActivityType, UserId.Value, 0, Id)).ToList();
            return GetRepetitionsFromVM(newReps, CompletedRepetitions, viewModelReps);
        }
    }
}
