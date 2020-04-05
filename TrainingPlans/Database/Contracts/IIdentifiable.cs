using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingPlans.Database.Contracts
{
    public interface IIdentifiable
    {
        public int Id { get; set; }
    }
}
