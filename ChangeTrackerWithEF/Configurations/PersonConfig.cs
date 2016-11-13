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
            config.HasMany(x => x.Cars).WithRequired(x => x.Owner);
            config.HasOptional(x => x.Address).WithRequired(x => x.Person);

            new TrackedEntityConfiguration<Person>().Configure(config);
        }
    }
}
