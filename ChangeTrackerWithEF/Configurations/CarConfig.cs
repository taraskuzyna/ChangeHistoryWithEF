using ChangeTrackerWithEF.Interfaces;
using ChangeTrackerWithEF.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ChangeTrackerWithEF.Configurations
{
    public class CarConfig : IEntityConfiguration<Car>
    {
        public void Configure(EntityTypeConfiguration<Car> config)
        {
            config.HasKey(x => x.Id);
            config
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            config.Property(x => x.Model).IsRequired();
            config.HasRequired(x => x.Owner);

            new TrackedEntityConfiguration<Car>().Configure(config);
        }
    }
}
