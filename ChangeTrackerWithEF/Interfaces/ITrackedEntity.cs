using ChangeTrackerWithEF.Attributes;
using System;

namespace ChangeTrackerWithEF.Interfaces
{
    public interface ITrackedEntity
    {
        [SkipTracking]
        string CreatedBy { get; set; }

        [SkipTracking]
        DateTime CreatedAt { get; set; }

        [SkipTracking]
        string ModifiedBy { get; set; }

        [SkipTracking]
        DateTime ModifiedAt { get; set; }
    }
}
