using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.AdditionalData;

namespace TrainingPlans.ViewModels.Validators
{
    public class PlannedWorkoutVMValidator : AbstractCustomValidator<PlannedWorkoutVM>
    {
        public PlannedWorkoutVMValidator()
        {
            RuleFor(x => x.ActivityType).NotNull().Custom(IsEnumValue<ActivityType>);

            RuleFor(x => x.ScheduledDate).NotNull()
                .Must(BeAValidDate).WithMessage("Not a valid date.");

            RuleFor(x => x.TimeOfDay).Custom(IsEnumValueOrNull<TimeOfDay>);

            RuleFor(x => x.WorkoutType).NotNull().Custom(IsEnumValue<WorkoutType>);
        }
    }
}
