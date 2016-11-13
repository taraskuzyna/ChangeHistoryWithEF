using System.Data.Entity;

namespace ChangeTrackerWithEF.Test
{
    public class DBbInitializer : DropCreateDatabaseAlways<ChangeTrackerDbContext>
    {
        protected override void Seed(ChangeTrackerDbContext context)
        {
            base.Seed(context);
        }
    }
}
