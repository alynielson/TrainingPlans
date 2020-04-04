using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.AdditionalData;

namespace TrainingPlans.ViewModels.Validators
{
    public class PlannedRepetitionVMValidator : AbstractRepetitionVMValidator<PlannedRepetitionVM>
    {
        public PlannedRepetitionVMValidator() : base()
        {
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
        }
    }
}
