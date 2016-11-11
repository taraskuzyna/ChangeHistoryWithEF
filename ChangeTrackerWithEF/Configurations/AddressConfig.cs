using ChangeTrackerWithEF.Interfaces;
using ChangeTrackerWithEF.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ChangeTrackerWithEF.Configurations
{
    public class AddressConfig : IEntityConfiguration<Address>
    {
        public void Configure(EntityTypeConfiguration<Address> config)
        {
            config.HasKey(x => x.Id);
            config
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            new TrackedEntityConfiguration<Address>().Configure(config);
        }
    }
    
}
