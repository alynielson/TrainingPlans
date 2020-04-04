using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingPlans.Database.Contracts
{
    public interface IOrderable
    {
        public int Order { get; set; }
    }
}
