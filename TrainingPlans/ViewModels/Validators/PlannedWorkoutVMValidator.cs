using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Common;
using TrainingPlans.Database.AdditionalData;

namespace TrainingPlans.ViewModels.Validators
{
    public class PlannedWorkoutVMValidator : AbstractCustomValidator<PlannedWorkoutVM>
    {
        public PlannedWorkoutVMValidator()
        {
            RuleFor(x => x.ScheduledDate).NotNull()
                .Must(BeAValidDate).WithMessage($"Not a valid date. Requires format {Constants.DateOnlyFormatString}.");
            RuleFor(x => x.CompletedDateTime).Must(BeAValidDateTimeOffset)
                .WithMessage($"Not a valid date. Requires format {Constants.DateTimeOffsetFormatString}.");
        }
    }
}
