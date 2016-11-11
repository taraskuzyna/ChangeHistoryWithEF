namespace ChangeTrackerWithEF.Interfaces
{
    interface ICar : ITrackedEntity
    {
        int Id { get; set; }

        string Model { get; set; }

        int Year { get; set; }
    }
}
