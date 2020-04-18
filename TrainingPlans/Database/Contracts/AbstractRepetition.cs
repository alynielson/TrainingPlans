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
    public abstract class AbstractRepetition : IOrderable, IIdentifiable
    {
        [Key]
        public int Id { get; set; }
        public double? DistanceQuantity { get; set; }
        public DistanceUom? DistanceUom { get; set; }
        public double? TimeQuantity { get; set; }
        public TimeUom? TimeUom { get; set; }
        public double? RestDistanceQuantity { get; set; }
        public DistanceUom? RestDistanceUom { get; set; }
        public double? RestTimeQuantity { get; set; }
        public TimeUom? RestTimeUom { get; set; }
        public int Order { get; set; }
        public string Notes { get; set; }
        public ActivityType ActivityType { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; } // This should be required, but making it nullable to not cause a bunch of sql cascade problems on deleting user.

        public AbstractRepetition() { }

        public AbstractRepetition(AbstractRepetitionVM viewModel, ActivityType activityType, int userId, int id)
        {
            Id = id;
            DistanceQuantity = viewModel.DistanceQuantity;
            DistanceUom = viewModel.DistanceUom;
            TimeQuantity = viewModel.TimeQuantity;
            TimeUom = viewModel.TimeUom;
            Notes = viewModel.Notes;
            RestDistanceQuantity = viewModel.RestDistanceQuantity;
            RestDistanceUom = viewModel.RestDistanceUom;
            RestTimeQuantity = viewModel.RestTimeQuantity;
            RestTimeUom = viewModel.RestTimeUom;
            Order = viewModel.Order;
            ActivityType = activityType;
            UserId = userId;
        }

        public virtual void UpdateFromWorkoutVM(AbstractRepetitionVM viewModel, ActivityType activityType)
        {
            DistanceQuantity = viewModel.DistanceQuantity;
            DistanceUom = viewModel.DistanceUom;
            TimeQuantity = viewModel.TimeQuantity;
            TimeUom = viewModel.TimeUom;
            Notes = viewModel.Notes;
            RestDistanceQuantity = viewModel.RestDistanceQuantity;
            RestDistanceUom = viewModel.RestDistanceUom;
            RestTimeQuantity = viewModel.RestTimeQuantity;
            RestTimeUom = viewModel.RestTimeUom;
            Order = viewModel.Order;
            ActivityType = activityType;
        }

        public virtual void UpdateFromVM(AbstractRepetitionVM viewModel)
        {
            DistanceQuantity = viewModel.DistanceQuantity;
            DistanceUom = viewModel.DistanceUom;
            TimeQuantity = viewModel.TimeQuantity;
            TimeUom = viewModel.TimeUom;
            Notes = viewModel.Notes;
            RestDistanceQuantity = viewModel.RestDistanceQuantity;
            RestDistanceUom = viewModel.RestDistanceUom;
            RestTimeQuantity = viewModel.RestTimeQuantity;
            RestTimeUom = viewModel.RestTimeUom;
        }
    }
}
