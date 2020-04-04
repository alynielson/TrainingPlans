using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingPlans.ViewModels.Validators
{
    public abstract class AbstractRepetitionVMValidator<T> : AbstractValidator<T> where T : AbstractRepetitionVM
    {
        public AbstractRepetitionVMValidator()
        {
            RuleFor(x => x.DistanceQuantity).Must((model, property) => ContainQuantityAndUnit(property, model.DistanceUom))
                .WithMessage("If present, must contain both quantity and unit of measure.")
                .Must((model, property) => ContainDistanceAndOrTime(property, model.TimeQuantity))
                .WithMessage("Distance and/or time are required.");
            RuleFor(x => x.TimeQuantity).Must((model, property) => ContainQuantityAndUnit(property, model.TimeUom))
                .WithMessage("If present, must contain both quantity and unit of measure.");
            RuleFor(x => x.RestDistanceQuantity).Must((model, property) => ContainQuantityAndUnit(property, model.RestDistanceQuantity))
                .WithMessage("If present, must contain both quantity and unit of measure.");
            RuleFor(x => x.RestTimeQuantity).Must((model, property) => ContainQuantityAndUnit(property, model.RestTimeQuantity))
                .WithMessage("If present, must contain both quantity and unit of measure.");
        }

        private bool ContainQuantityAndUnit<TEnum>(double? quantity, TEnum? unit) where TEnum : struct, IConvertible
        {
            return (quantity.HasValue && unit.HasValue) || (!quantity.HasValue && !unit.HasValue);
        }

        private bool ContainDistanceAndOrTime(double? distanceQuantity, double? timeQuantity)
        {
            return (distanceQuantity.HasValue || timeQuantity.HasValue);
        }
    }
}
