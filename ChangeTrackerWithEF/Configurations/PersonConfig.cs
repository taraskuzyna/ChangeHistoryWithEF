using ChangeTrackerWithEF.Interfaces;
using ChangeTrackerWithEF.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ChangeTrackerWithEF.Configurations
{
    public class PersonConfig : IEntityConfiguration<Person>
    {
        public void Configure(EntityTypeConfiguration<Person> config)
        {
            config.HasKey(x => x.Id);
            config
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            config.HasMany(x => x.Cars).WithRequired(x => x.Owner);
            config.HasOptional(x => x.Address);

            new TrackedEntityConfiguration<Person>().Configure(config);
        }
    }
}
