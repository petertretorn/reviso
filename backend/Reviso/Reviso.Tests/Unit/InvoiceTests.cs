using Reviso.Domain.Entities;
using Reviso.Domain.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Reviso.Tests
{
    public class InvoiceTests
    {
        [Fact]
        public void Can_Calculate_Flatrate_Invoice()
        {
            //arrange
            Project project = BuildProject();

            var intervals = new List<RateInterval>
            {
                new RateInterval
                {
                    FromHours = 0,
                    ToHours = Int32.MaxValue,
                    DiscountFactor = 0
                }
            };

            var registrations = new List<TimeRegistration> 
            {
                new TimeRegistration { Date = DateTime.Now.Date, Hours = 6 },
                new TimeRegistration { Date = DateTime.Now.Date, Hours = 8 },
                new TimeRegistration { Date = DateTime.Now.Date, Hours = 10 },
                new TimeRegistration { Date = DateTime.Now.Date, Hours = 20 }
            };

            SetupProject(project, intervals, registrations);

            //act
            var sut = new CalculateService(new CalculateStrategy());
            var invoice = sut.CalculateInvoice(project);

            //assert
            var actual = invoice.Net;
            var expected = (6 + 8 + 10 + 20) * 100;

            Assert.Equal(expected, actual);
        }

        

        [Fact]
        public void Can_Calculate_Invoice_With_Differentiated_Rates()
        {
            //arrange
            Project project = BuildProject();

            project.Contract.RateIntervals = new List<RateInterval>
            {
                new RateInterval
                {
                    FromHours = 0,
                    ToHours = 10,
                    DiscountFactor = 0
                },
                new RateInterval
                {
                    FromHours = 11,
                    ToHours = Int32.MaxValue,
                    DiscountFactor = .1m
                }
            };

            project.AddTimeRegistration(new TimeRegistration { Date = DateTime.Now.Date, Hours = 6 });
            project.AddTimeRegistration(new TimeRegistration { Date = DateTime.Now.Date, Hours = 8 });
            project.AddTimeRegistration(new TimeRegistration { Date = DateTime.Now.Date, Hours = 6 });

            project.Close();

            var sut = new CalculateService(new CalculateStrategy());

            //act
            var invoice = sut.CalculateInvoice(project);

            //assert
            var actual = invoice.Net;
            var expected = (10 * 100) + (10 * (1 - 0.1m) * 100);

            Assert.Equal(expected, actual);

        }

        private Project BuildProject()
        {
            var contract = new Contract()
            {
                BaseRate = 100,
            };

            var project = new Project
            {
                Contract = contract,
                Start = DateTime.Now.Date
            };

            return project;
        }

        private void SetupProject(Project project, List<RateInterval> intervals, List<TimeRegistration> registrations)
        {
            foreach (var registration in registrations)
            {
                project.AddTimeRegistration(registration);
            }

            project.Contract.RateIntervals = intervals;

            project.Close();
        }
    }
}
