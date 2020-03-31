using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingPlans.ViewModels.Validators
{
    public interface ICustomValidator<T>
    {
        void PerformValidation(T instance);
    }
}
