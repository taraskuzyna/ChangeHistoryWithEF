using System;
using ChangeTrackerWithEF.Interfaces;

namespace ChangeTrackerWithEF.Models
{
    public class Address : IAddress
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string ModifiedBy { get; set; }

        public virtual Person Person { get; set; }
    }
}