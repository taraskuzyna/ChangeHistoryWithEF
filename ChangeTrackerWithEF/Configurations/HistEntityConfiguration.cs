using ChangeTrackerWithEF.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ChangeTrackerWithEF.Configurations
{
    public class HistEntityConfiguration<T> : IEntityConfiguration<T>
        where T : class, IHistEntity, ITrackedEntity
    {
        public void Configure(EntityTypeConfiguration<T> config)
        {
            config
               .HasKey(e => e.HistoryId);

            config
                .Property(e => e.HistoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            new TrackedEntityConfiguration<T>().Configure(config);
        }
    }
}
