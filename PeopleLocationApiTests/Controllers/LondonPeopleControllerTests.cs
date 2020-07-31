using PeopleLocationApi.Controllers;
using Shouldly;
using Xunit;

namespace PeopleLocationApiTests.Controllers
{
    public class LondonPeopleControllerTests
    {
        [Fact]
        public void GetShouldReturnValue()
        {
            // Arrange
            var controller = new LondonPeopleController();

            // Act
            var response = controller.Get();

            // Assert
            response.ShouldNotBeEmpty();

        }
    }
}
