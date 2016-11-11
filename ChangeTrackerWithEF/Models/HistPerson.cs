using ChangeTrackerWithEF.Interfaces;
using System;
using System.Collections.Generic;

namespace ChangeTrackerWithEF.Models
{
    public class HistPerson : IPerson, IHistEntity
    {
        public int HistoryId { get; set; }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public virtual Address Address { get; set; }

        public virtual IEnumerable<Car> Cars { get; set; }
    }
}
