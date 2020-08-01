using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PeopleLocationApi.Constants;
using PeopleLocationApi.Controllers;
using PeopleLocationApi.Models;
using PeopleLocationApi.Tasks;
using Shouldly;
using Xunit;

namespace PeopleLocationApiTests.Controllers
{
    public class LondonPeopleControllerTests
    {
        [Fact]
        public async void PeopleLivingInLondonShouldReturnOkWithAListOfPeople()
        {
            // Arrange
            var people = new List<Person> {Person.Create(1, "firstName1", "lastName1", "test@me.com")};
            var mock = new Mock<IPeopleLocationTask>();
            mock.Setup(x => x.GetPeopleLivingIn(LondonCityConstants.Name)).ReturnsAsync(people).Verifiable();

            var controller = new LondonPeopleController(mock.Object);

            // Act
            var result = await controller.GetPeopleLivingInLondon() as OkObjectResult;

            // Assert 
            mock.Verify(x => x.GetPeopleLivingIn(LondonCityConstants.Name), Times.Once);
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            ((IEnumerable<Person>) result.Value).Count().ShouldBe(people.Count);
        }

        [Fact]
        public async Task PeopleLivingInLondonShouldReturnNotFound()
        {
            // Arrange
            var mock = new Mock<IPeopleLocationTask>();
            mock.Setup(x => x.GetPeopleLivingIn(LondonCityConstants.Name)).ReturnsAsync((IEnumerable<Person>) null)
                .Verifiable();
            var controller = new LondonPeopleController(mock.Object);

            // Act
            var result = await controller.GetPeopleLivingInLondon() as NotFoundResult;

            // Assert 
            mock.Verify(x => x.GetPeopleLivingIn(LondonCityConstants.Name), Times.Once);
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public async Task PeopleLivingWithInFiftyMilesOfLondonShouldReturnOkWithAListOfPeople()
        {
            // Arrange
            const double miles = 50;
            var people = new List<Person> {Person.Create(1, "firstName1", "lastName1", "test@me.com")};
            var mock = new Mock<IPeopleLocationTask>();
            mock.Setup(x => x.GetPeopleLivingWithIn(LondonCityConstants.Name, miles)).ReturnsAsync(people).Verifiable();

            var controller = new LondonPeopleController(mock.Object);

            // Act
            var result = await controller.GetPeopleLivingWithInFiFtyMilesOfLondon() as OkObjectResult;

            // Assert 
            mock.Verify(x => x.GetPeopleLivingWithIn(LondonCityConstants.Name, miles), Times.Once);
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            ((IEnumerable<Person>) result.Value).Count().ShouldBe(people.Count);
        }

        [Fact]
        public async Task PeopleLivingWithInFiftyMilesOfLondonShouldReturnNotFound()
        {
            // Arrange
            const double miles = 50;
            var mock = new Mock<IPeopleLocationTask>();
            mock.Setup(x => x.GetPeopleLivingWithIn(LondonCityConstants.Name, 50)).ReturnsAsync((IEnumerable<Person>) null)
                .Verifiable();
            var controller = new LondonPeopleController (mock.Object);

            // Act
            var result = await controller.GetPeopleLivingWithInFiFtyMilesOfLondon() as NotFoundResult;
            
            // Assert
            mock.Verify(x => x.GetPeopleLivingWithIn(LondonCityConstants.Name, miles), Times.Once);
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }
    }
}