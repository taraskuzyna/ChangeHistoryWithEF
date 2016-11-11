using System;
using ChangeTrackerWithEF.Interfaces;

namespace ChangeTrackerWithEF.Models
{
    public class HistAddress : IAddress, IHistEntity
    {
        public int HistoryId { get; set; }

        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string ModifiedBy { get; set; }
    }
}