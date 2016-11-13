using ChangeTrackerWithEF.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ChangeTrackerWithEF.Test
{
    [TestFixture]
    public class ChangeTrackerTest
    {
        ChangeTrackerDbContext context;

        [OneTimeSetUp]
        public void Init()
        {
            Database.SetInitializer(new DBbInitializer());
            context = new ChangeTrackerDbContext();
        }

        [Test, Order(1)]
        public void ShouldInsertPersonWithoutHistory()
        {
            Person p = new Person()
            {
                FirstName = "Taras",
                LastName = "Kuzyna",
                Address = new Address()
                {
                    City = "Wroclaw",
                    Street = "Pilsuckiego",
                    ZipCode = "21-123"
                },
                Cars = new List<Car>()
                {
                    new Car()
                    {
                        Model = "Ferrari"
                    }
                }
            };

            context.Persons.Add(p);
            context.SaveChanges();

            Person p1 = context.Persons.Single();
            HistPerson hp = context.HistPersons.FirstOrDefault(x => x.Id == p1.Id);

            Assert.NotNull(p1.CreatedBy, "Person.CreatedBy is not null");
            Assert.NotNull(p1.CreatedAt, "Person.CreatedAt is not null");
            Assert.NotNull(p1.ModifiedAt, "Person.ModifiedAt is not null");
            Assert.NotNull(p1.ModifiedBy, "Person.ModifiedBy is not null");

            Assert.NotNull(p1.Address.CreatedBy, "Person.Address.CreatedBy is not null");
            Assert.NotNull(p1.Address.CreatedAt, "Person.Address.CreatedAt is not null");
            Assert.NotNull(p1.Address.ModifiedAt, "Person.Address.ModifiedAt is not null");
            Assert.NotNull(p1.Address.ModifiedBy, "Person.Address.ModifiedBy is not null");

            Assert.NotNull(p1.Cars.Single().CreatedAt, "Person.Car.CreatedBy is not null");
            Assert.NotNull(p1.Cars.Single().CreatedAt, "Person.Car.CreatedAt is not null");
            Assert.NotNull(p1.Cars.Single().ModifiedAt, "Person.Car.ModifiedAt is not null");
            Assert.NotNull(p1.Cars.Single().ModifiedBy, "Person.Car.ModifiedBy is not null");

            Assert.Null(hp, "History person is null");

        }

        [Test, Order(2)]
        public void ShouldUpdatePersonAndCreateHistoryRecords()
        {
            Person p = context.Persons.Single();
            p.FirstName = "Tarzan";
            context.SaveChanges();

            Person p1 = context.Persons.Single();
            HistPerson ph = context.HistPersons.Single();

            Assert.NotNull(p1.CreatedBy, "Person.CreatedBy is not null");
            Assert.NotNull(p1.CreatedAt, "Person.CreatedAt is not null");
            Assert.NotNull(p1.ModifiedAt, "Person.ModifiedAt is not null");
            Assert.NotNull(p1.ModifiedBy, "Person.ModifiedBy is not null");
            Assert.AreNotEqual(p1.ModifiedAt, p1.CreatedAt, "Person.ModifiedAt is distinct from Person.CreatedAt");
            Assert.AreEqual(p1.ModifiedBy, p1.CreatedBy, "Person.ModifiedBy is not distinct from Person.CreatedBy");
            Assert.NotNull(ph, "History person is not null");
        }

        [Test, Order(3)]
        public void ShouldUpdateCascadeAndCreateHistory()
        {

            Person p = context.Persons.Single();
            p.Address.Street = "Pereca";
            p.Cars.Add(new Car() { Model = "Mustang" });
            context.SaveChanges();

            Person p1 = context.Persons.Single();
            HistPerson ph = context.HistPersons.Single();
            HistAddress ah = context.HistAddresses.Single();
            HistCar ch = context.HistCars.SingleOrDefault();

            Assert.NotNull(p1.Address.CreatedBy, "Person.Address.CreatedBy is not null");
            Assert.NotNull(p1.Address.CreatedAt, "Person.Address.CreatedAt is not null");
            Assert.NotNull(p1.Address.ModifiedAt, "Person.Address.ModifiedAt is not null");
            Assert.NotNull(p1.Address.ModifiedBy, "Person.Address.ModifiedBy is not null");
            Assert.AreNotEqual(p1.Address.ModifiedAt, p1.Address.CreatedAt, "Person.Address.ModifiedAt is distinct from Person.Address.CreatedAt");
            Assert.AreEqual(p1.Address.ModifiedBy, p1.Address.CreatedBy, "Person.Address.ModifiedBy is not distinct from Person.Address.CreatedBy");
            Assert.NotNull(ph, "Hist person single and is not null");
            Assert.NotNull(ah, "Hist address single and is not null");
            Assert.Null(ch, "Hist car is null");
        }

        [Test, Order(4)]
        public void ShouldDeleteCascadeAndCreateHistory()
        {

            Person p = context.Persons.Single();
            context.Persons.Remove(p);
            context.SaveChanges();

            Person p1 = context.Persons.SingleOrDefault();
            Address a = context.Addresses.SingleOrDefault();
            Car c = context.Cars.SingleOrDefault();
            List<HistPerson> phlist = context.HistPersons.ToList();
            List<HistAddress> ahlist = context.HistAddresses.ToList();
            List<HistCar> chlist = context.HistCars.ToList();

            Assert.Null(p1, "Person is null");
            Assert.Null(a, "Address is null");
            Assert.Null(c, "Car is null");
            Assert.NotNull(phlist, "Person history is not null");
            Assert.NotNull(ahlist, "Address history is not null");
            Assert.NotNull(chlist, "Car history is not null");
            Assert.AreEqual(2, phlist.Count, "Person history has 2 rows");
            Assert.AreEqual(2, ahlist.Count, "Address history has 2 rows");
            Assert.AreEqual(2, chlist.Count, "Car history has 2 rows");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
