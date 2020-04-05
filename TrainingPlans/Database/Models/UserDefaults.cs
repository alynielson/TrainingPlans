using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.AdditionalData;

namespace TrainingPlans.Database.Models
{
    public class UserDefaults
    {
        [Key]
        public int Id { get; set; }
        public DistanceUom DistanceUom { get; set; }
        public TimeUom TimeUom { get; set; }
        public bool IsPaceDistancePerTime { get; set; }
        public double Pace { get; set; }
        public ActivityType ActivityType { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
