using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace TrainingPlans.ViewModels.Validators
{
    public abstract class AbstractWorkoutVMValidator<T> : AbstractValidator<T> where T : AbstractWorkoutVM
    {
    }
}
