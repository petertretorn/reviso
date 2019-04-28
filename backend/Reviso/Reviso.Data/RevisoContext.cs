using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using Reviso.Domain.Entities;

namespace Reviso.Data
{
    public class RevisoContext: DbContext
    {
        readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Reviso;Trusted_Connection=True;";

        RevisoContext(DbContextOptions<RevisoContext> options) : base(options) { }

        public RevisoContext() : base() { }

        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // project 1
            var project1 = new Project
            {
                Id = 1,
                Name = "Portal Solution",
                Start = new DateTime(2019, 4, 1),
            };

            var contract1 = new Contract
            {
                Id = 1,
                ProjectId = 1,
                BaseRate = 100,
                VatRate = 0.22m,
            };

            var rateInterval1 = new RateInterval
            {
                Id = 1,
                FromHours = 0,
                ToHours = Int32.MaxValue,
                DiscountFactor = 0,
                ContractId = 1
            };

            var customer1 = new Customer
            {
                Id = 1,
                ContractId = 1,
                Name = "Acme A/S"
            };

            var registrations1 = new List<TimeRegistration>
            {
                new TimeRegistration { Id = 1, Date = new DateTime(2019, 4, 25), Hours = 6, ProjectId = 1 },
                new TimeRegistration { Id = 2, Date = new DateTime(2019, 4, 26), Hours = 8, ProjectId = 1 },
                new TimeRegistration { Id = 3, Date = new DateTime(2019, 4, 27), Hours = 7, ProjectId = 1 }
            };

            //project 2
            var project2 = new Project
            {
                Id = 2,
                Name = "New Version 2.0",
                Start = new DateTime(2019, 2, 15),
            };

            var contract2 = new Contract
            {
                Id = 2,
                ProjectId = 2,
                BaseRate = 110,
                VatRate = 0.18m,
            };

            var rateInterval2 = new RateInterval
            {
                Id = 2,
                FromHours = 0,
                ToHours = Int32.MaxValue,
                DiscountFactor = 0,
                ContractId = 2
            };

            var customer2 = new Customer
            {
                Id = 2,
                ContractId = 2,
                Name = "Insurance Inc."
            };

            var registrations2 = new List<TimeRegistration>
            {
                new TimeRegistration { Id = 4, Date = new DateTime(2019, 4, 26), Hours = 4, ProjectId = 2 },
                new TimeRegistration { Id = 5, Date = new DateTime(2019, 4, 27), Hours = 9, ProjectId = 2 },
            };

            modelBuilder.Entity<Project>().HasData(project1, project2);
            modelBuilder.Entity<Contract>().HasData(contract1, contract2);
            modelBuilder.Entity<Customer>().HasData(customer1, customer2);
            modelBuilder.Entity<RateInterval>().HasData(rateInterval1, rateInterval2);
            modelBuilder.Entity<TimeRegistration>().HasData(registrations1);
            modelBuilder.Entity<TimeRegistration>().HasData(registrations2);
        }
    }
}
