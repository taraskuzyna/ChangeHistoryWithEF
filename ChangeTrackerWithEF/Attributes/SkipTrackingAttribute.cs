using System;

namespace ChangeTrackerWithEF.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SkipTrackingAttribute : Attribute
    {

    }
}
