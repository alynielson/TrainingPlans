using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Contracts;

namespace TrainingPlans.ViewModels
{
    public class PlannedWorkoutDateUpdate
    {
        public TimeOfDay TimeOfDay { get; set; }
        public string ScheduledDate { get; set; }
        public IReadOnlyList<PlannedWorkoutOrder> WorkoutOrders { get; set; }
    }

    public class PlannedWorkoutOrder : IOrderable
    {
        public int WorkoutId { get; set; }
        public int Order { get; set; }
    }
}
