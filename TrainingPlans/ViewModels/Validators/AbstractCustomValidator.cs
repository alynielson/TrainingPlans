using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Contracts;
using TrainingPlans.ExceptionHandling;

namespace TrainingPlans.ViewModels.Validators
{
    public abstract class AbstractCustomValidator<T> : AbstractValidator<T>
    {
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

        protected bool BeDistinct(IReadOnlyList<IOrderable> orderableCollection)
        {
            if (orderableCollection is null) return true;
            var orderValues = orderableCollection.Select(x => x.Order).ToList();
            return orderValues.Distinct().ToList().Count == orderValues.Count;
        }
    }
}
