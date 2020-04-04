using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TrainingPlans.Common;

namespace TrainingPlans.ViewModels.Validators
{
    public class CompletedWorkoutVMValidator : AbstractWorkoutVMValidator<CompletedWorkoutVM>
    {
        public CompletedWorkoutVMValidator()
        {
            RuleFor(x => x.CompletedDateTime).NotNull()
                .Must(ValidatorHelpers.BeAValidDate).WithMessage($"Not a valid date. Requires format {Constants.DateTimeOffsetFormatString}.");
            RuleForEach(x => x.CompletedRepetitions).InjectValidator((x, y) => new CompletedRepetitionVMValidator());
            RuleFor(x => x.CompletedRepetitions).Must(ValidatorHelpers.BeDistinct)
                .WithMessage("Repetitions could not be put in order.");
        }
    }
}
