using System;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;
using TrainingPlans.Common;
using TrainingPlans.Database.Contracts;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrainingPlans.ViewModels
{
    public class PlannedRepetitionVM : AbstractRepetitionVM
    {
        public int Quantity { get; set; } = 1;

        public PlannedRepetitionVM(PlannedRepetition model, UserDefaults defaults = null) : base(model, defaults)
        {
            Quantity = model.Quantity;
        }
        public PlannedRepetitionVM() : base() { }
    }
}
