using System.Data.Entity.ModelConfiguration;

namespace ChangeTrackerWithEF.Interfaces
{
    internal interface IEntityConfiguration<T>
         where T : class
    {
        void Configure(EntityTypeConfiguration<T> config);
    }
}
