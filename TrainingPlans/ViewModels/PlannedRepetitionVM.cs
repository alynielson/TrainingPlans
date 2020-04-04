﻿using System;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;
using TrainingPlans.Common;
using TrainingPlans.Database.Contracts;

namespace TrainingPlans.ViewModels
{
    public class PlannedRepetitionVM : AbstractRepetitionVM
    {
        public int Quantity { get; set; } = 1;

        public PlannedRepetitionVM(PlannedRepetition model)
        {
            Id = model.Id;
            DistanceQuantity = model.DistanceQuantity;
            DistanceUom = model.DistanceUom;
            TimeQuantity = model.TimeQuantity;
            TimeUom = model.TimeUom;
            Notes = model.Notes;
            Quantity = model.Quantity;
            RestDistanceQuantity = model.RestDistanceQuantity;
            RestDistanceUom = model.RestDistanceUom;
            RestTimeQuantity = model.RestTimeQuantity;
            RestTimeUom = model.RestTimeUom;
            Order = model.Order;
            SetPaces();
        }
        public PlannedRepetitionVM() { }
    }
}
