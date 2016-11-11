namespace ChangeTrackerWithEF.Interfaces
{
    public interface IPerson : ITrackedEntity
    {
        int Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }
    }
}
