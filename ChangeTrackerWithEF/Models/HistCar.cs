using System;
using ChangeTrackerWithEF.Interfaces;

namespace ChangeTrackerWithEF.Models
{
    public class HistCar : ICar, IHistEntity
    {
        public int HistoryId { get; set; }

        public int Id { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public virtual Person Owner { get; set; }
    }
}