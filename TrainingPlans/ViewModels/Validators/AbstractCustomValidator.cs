using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.ExceptionHandling;

namespace TrainingPlans.ViewModels.Validators
{
    public abstract class AbstractCustomValidator<T> : AbstractValidator<T>, ICustomValidator<T>
    {
        public virtual void PerformValidation(T instance)
        {
            var result = base.Validate(instance);

            if (!result.IsValid)
            {
                var messages = result.Errors?.ToDictionary(x => $"{x.PropertyName}", x => new[] { $"{x.ErrorMessage}" });
                throw new InvalidModelException(messages);
            }
        }

        protected bool BeAValidDate(string value)
        {
            if (value is null) return true; // Use .NotNull() in validator before this to validate that a value is not null
            return DateTime.TryParse(value, out var _);
        }

        protected bool BeAValidDateTimeOffset(string value)
        {
            if (value is null) return true; // Use .NotNull() in validator before this to validate that a value is not null
            return DateTimeOffset.TryParse(value, out var _);
        }
    }
}
