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
    public class PlannedWorkoutVMValidator : AbstractWorkoutVMValidator<PlannedWorkoutVM>
    {
        public PlannedWorkoutVMValidator() : base()
        {
            RuleFor(x => x.ScheduledDate).NotNull()
                .Must(ValidatorHelpers.BeAValidDate).WithMessage($"Not a valid date. Requires format {Constants.DateOnlyFormatString}.");
            RuleForEach(x => x.PlannedRepetitions).InjectValidator((x, y) => new PlannedRepetitionVMValidator());
            RuleFor(x => x.PlannedRepetitions).NotEmpty();
            RuleFor(x => x.PlannedRepetitions).Must(ValidatorHelpers.BeDistinct)
                .WithMessage("Repetitions could not be put in order.");
        }
    }
}
