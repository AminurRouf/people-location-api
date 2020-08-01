using PeopleLocationApi.Helpers;
using PeopleLocationApi.Models;
using Shouldly;
using Xunit;

namespace PeopleLocationApiTests.Helpers
{
    public class JsonConvertorHelperTests
    {
        [Fact]
        public void DeserializeContentShouldHandlePrivateSetterAndMapSnakeCaseToModel()
        {
            // Arrange
            var content = @" {'first_name': 'Johnny' }";
            
            // Act
            var result = JsonConvertorHelper.DeserializeContent<Person>(content);
           
            // Assert
            result.FirstName.ShouldBe("Johnny");
        }
    }

}

