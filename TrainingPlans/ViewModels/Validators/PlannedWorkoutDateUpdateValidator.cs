using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TrainingPlans.Common;

namespace TrainingPlans.ViewModels.Validators
{
    public class PlannedWorkoutDateUpdateValidator : AbstractValidator<PlannedWorkoutDateUpdate>
    {
        public PlannedWorkoutDateUpdateValidator() : base()
        {
            RuleFor(x => x.ScheduledDate).NotNull()
                .Must(ValidatorHelpers.BeAValidDate).WithMessage($"Not a valid date. Requires format {Constants.DateOnlyFormatString}.");
            RuleFor(x => x.WorkoutOrders).NotEmpty().Must(ValidatorHelpers.BeDistinct)
                .WithMessage("Workouts could not be put in order.");
        }
    }
}
