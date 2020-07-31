using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PeopleLocationApi.Controllers;
using PeopleLocationApi.Models;
using PeopleLocationApi.Services;
using Shouldly;
using Xunit;

namespace PeopleLocationApiTests.Controllers
{
    public class LondonPeopleControllerTests
    {
        [Fact]
        public async void GetPeopleLivingInLondonShouldOkWithAListOfPeople()
        {
            // Arrange
            var people = new List<Person> {Person.Create(1, "firstName1", "lastName1", "test@me.com")};
            var mock = new Mock<IBpdtsTestAppService>();
            mock.Setup(x => x.GetPeopleLivingInLondon()).ReturnsAsync(people).Verifiable();
            var controller = new LondonPeopleController(mock.Object);

            // Act
            var result = await controller.GetPeopleLivingInLondon() as OkObjectResult;
            mock.Verify();

            // Assert 
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            ((IEnumerable<Person>) result.Value).Count().ShouldBe(people.Count);
        }

        [Fact]
        public async Task GetPeopleLivingInLondonShouldReturnNotFound()
        {
            // Arrange
            var mock = new Mock<IBpdtsTestAppService>();
            mock.Setup(x => x.GetPeopleLivingInLondon()).ReturnsAsync((IEnumerable<Person>) null).Verifiable();
            var controller = new LondonPeopleController(mock.Object);

            // Act
            var result = await controller.GetPeopleLivingInLondon() as NotFoundResult;
            mock.Verify();

            // Assert 
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }
    }
}