using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.AdditionalData;

namespace TrainingPlans.ViewModels.Validators
{
    public class PlannedRepetitionVMValidator : AbstractCustomValidator<PlannedRepetitionVM>
    {
        public PlannedRepetitionVMValidator()
        {
            RuleFor(x => x.DistanceUom).Custom(IsEnumValueOrNull<DistanceUom>);
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
            RuleFor(x => x.TimeUom).Custom(IsEnumValueOrNull<TimeUom>);
        }
    }
}
