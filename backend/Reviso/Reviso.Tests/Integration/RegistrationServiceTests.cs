using Reviso.Application;
using Reviso.Data;
using Reviso.Domain.Entities;
using System.Linq;
using Xunit;

namespace Reviso.Tests.Integration
{
    public class RegistrationServiceTests
    {
        [Fact]
        public void Can_Fetch_Registrations_For_Project()
        {
            //arrange
            var sut = new RegistrationService( 
                    new Repository<Project>( new RevisoContext() ), 
                    new Repository<TimeRegistration>(new RevisoContext())
                );

            //act
            var registrations = sut.GetRegistrationsForProject(1);

            //assert
            Assert.NotEmpty(registrations);

            var count = registrations.Count();
            var shouldBeTrue = count > 0;

            Assert.True(shouldBeTrue);
        }
    }
}
