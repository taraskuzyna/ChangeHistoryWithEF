using ChangeTrackerWithEF.Interfaces;
using System.Data.Entity.ModelConfiguration;

namespace ChangeTrackerWithEF.Configurations
{
    public class TrackedEntityConfiguration<T> : IEntityConfiguration<T>
        where T : class, ITrackedEntity
    {
        public void Configure(EntityTypeConfiguration<T> config)
        {
            config
                .Property(e => e.CreatedBy)
                .IsRequired();

            config
                .Property(e => e.CreatedAt)
                .IsRequired();

            config
                .Property(e => e.ModifiedBy)
                .IsRequired();

            config
                .Property(e => e.ModifiedAt)
                .IsRequired();
        }
    }
}
