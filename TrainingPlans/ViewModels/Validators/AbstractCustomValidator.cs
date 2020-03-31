using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingPlans.ViewModels.Validators
{
    public abstract class AbstractCustomValidator<T> : AbstractValidator<T>
    {
        protected void IsEnumValue<TEnum>(string value, CustomContext context)
        {
            if (!Enum.TryParse(typeof(TEnum), value, out var _))
                context.AddFailure("Not valid value.");
        }

        protected void IsEnumValueOrNull<TEnum>(string value, CustomContext context)
        {
            if (value is null)
                return;
            IsEnumValue<TEnum>(value, context);
        }

        protected bool BeAValidDate(string value)
        {
            return DateTime.TryParse(value, out var _);
        }
    }
}
