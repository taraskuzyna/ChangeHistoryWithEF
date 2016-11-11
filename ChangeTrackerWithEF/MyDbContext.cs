using AutoMapper;
using ChangeTrackerWithEF.Attributes;
using ChangeTrackerWithEF.Configurations;
using ChangeTrackerWithEF.Interfaces;
using ChangeTrackerWithEF.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ChangeTrackerWithEF
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("TestDb")
        {

        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<HistPerson> HistPersons { get; set; }

        public DbSet<HistCar> HistCars { get; set; }

        public DbSet<HistAddress> HistAddresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            new PersonConfig().Configure(modelBuilder.Entity<Person>());
            new CarConfig().Configure(modelBuilder.Entity<Car>());
            new AddressConfig().Configure(modelBuilder.Entity<Address>());
            new HistEntityConfiguration<HistPerson>().Configure(modelBuilder.Entity<HistPerson>());
            new HistEntityConfiguration<HistCar>().Configure(modelBuilder.Entity<HistCar>());
            new HistEntityConfiguration<HistAddress>().Configure(modelBuilder.Entity<HistAddress>());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();

            if (this.ChangeTracker.Entries().Where(s => s.State == EntityState.Modified || s.State == EntityState.Added).Any())
            {
                DateTime now = DateTime.Now;
                string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                // for each added or modified entity set the same tracked info 
                foreach (DbEntityEntry entry in this.ChangeTracker.Entries().Where(s => s.State == EntityState.Modified || s.State == EntityState.Added))
                {
                    if (entry.Entity is ITrackedEntity)
                    {
                        if (entry.State == EntityState.Modified)
                        {
                            (entry.Entity as ITrackedEntity).ModifiedBy = user;
                            (entry.Entity as ITrackedEntity).ModifiedAt = now;
                        }
                        else
                        {
                            (entry.Entity as ITrackedEntity).CreatedBy = user;
                            (entry.Entity as ITrackedEntity).CreatedAt = now;
                            (entry.Entity as ITrackedEntity).ModifiedBy = user;
                            (entry.Entity as ITrackedEntity).ModifiedAt = now;
                        }
                    }
                }
            }

            if (this.ChangeTracker.Entries().Where(s => s.State == EntityState.Modified || s.State == EntityState.Deleted).Any())
            {
                foreach (DbEntityEntry entry in this.ChangeTracker.Entries().Where(s => s.State == EntityState.Modified || s.State == EntityState.Deleted))
                {
                    Type poco = ObjectContext.GetObjectType(entry.Entity.GetType());

                    // base interface of POCO 
                    Type baseInterface = poco.GetInterfaces().Single(i => i.Name.Equals($"I{poco.Name}"));

                    // type of class which implememt base interface and IHistEntity
                    Type histPoco = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                        .Where(c => baseInterface.IsAssignableFrom(c) && typeof(IHistEntity).IsAssignableFrom(c)).Single();

                    if (EntityHasChanges(entry, poco))
                    {
                        var histEntity = Activator.CreateInstance(histPoco);
                        Mapper.Map(entry.OriginalValues.ToObject(), histEntity, poco, histPoco);
                        if (histEntity != null)
                            this.Set(histPoco).Add(histEntity);
                    }
                }
            }
            return base.SaveChanges();
        }

        private bool EntityHasChanges(DbEntityEntry entry, Type poco)
        {
            if (entry.State == EntityState.Deleted)
                return true;

            foreach (string prop in entry.OriginalValues.PropertyNames)
            {
                if (entry.OriginalValues[prop] != entry.CurrentValues[prop] &&
                    !poco.GetProperty(prop).GetCustomAttributes(typeof(SkipTrackingAttribute), true).Any())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
