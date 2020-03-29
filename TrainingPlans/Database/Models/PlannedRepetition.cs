using System.ComponentModel.DataAnnotations;
using TrainingPlans.Database.AdditionalData;

namespace TrainingPlans.Database.Models
{
    public class PlannedRepetition
    {
        [Key]
        public int Id { get; set; }
        public double? DistanceQuantity { get; set; }
        public DistanceUom? DistanceUom { get; set; }
        public double? TimeQuantity { get; set; }
        public TimeUom? TimeUom { get; set; }
        public string Notes { get; set; }
        public int Quantity { get; set; } = 1;
        public PlannedWorkout PlannedWorkout { get; set; }
    }
}
