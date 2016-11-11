namespace ChangeTrackerWithEF.Interfaces
{
    public interface IAddress : ITrackedEntity
    {
        int Id { get; set; }

        string Street { get; set; }

        string City { get; set; }

        string ZipCode { get; set; }
    }
}
