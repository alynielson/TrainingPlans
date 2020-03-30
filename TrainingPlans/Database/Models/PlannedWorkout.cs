﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using TrainingPlans.Common;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Database.Models
{
    public class PlannedWorkout
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeOfDay TimeOfDay { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int Order { get; set; }
        public ActivityType ActivityType { get; set; }
        public WorkoutType WorkoutType { get; set; }
        public int AthleteId { get; set; }
        [ForeignKey("AthleteId")]
        public User Athlete { get; set; }
        public List<PlannedRepetition> PlannedRepetitions { get; set; }

        public PlannedWorkout() { }
        
        public PlannedWorkout(PlannedWorkoutVM viewModel)
        {
            Id = viewModel.Id;
            Name = viewModel.Name;
            TimeOfDay = viewModel.TimeOfDay is null ? TimeOfDay.Any : viewModel.TimeOfDay.ValidateEnum<TimeOfDay>();
            ScheduledDate = viewModel.ScheduledDate.ValidateDate();
            Order = viewModel.Order;
            ActivityType = viewModel.ActivityType.ValidateEnum<ActivityType>();
            WorkoutType = viewModel.WorkoutType.ValidateEnum<WorkoutType>();
            PlannedRepetitions = viewModel.PlannedRepetitions.Select(x => new PlannedRepetition(x)).ToList();
        }
    }
}