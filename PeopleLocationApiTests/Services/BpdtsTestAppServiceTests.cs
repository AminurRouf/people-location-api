using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using PeopleLocationApi.Constants;
using PeopleLocationApi.Services;
using Shouldly;
using Xunit;

namespace PeopleLocationApiTests.Services
{
    public class BpdtsTestAppServiceTests
    {
        [Fact]
        public async void ShouldReturnPeopleLivingAPlace()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{ ""id"": 1, 
                                                ""first_name"": ""firstname1"", 
                                                ""last_name"": ""lastname2"",
                                                ""email"": ""test@email.com""}]")
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object) {BaseAddress = new Uri("http://anyoldurl/")};
            var sut = new BpdtsTestAppService(httpClient);

            var result = await sut.GetPeopleLivingIn(LondonCityConstants.Name);

           result.ShouldNotBeNull();
        }

        [Fact]
        public async void ShouldReturnUsers()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{ ""id"": 1, 
                                                ""first_name"": ""firstname1"", 
                                                ""last_name"": ""lastname2"",
                                                ""email"": ""test@email.com""}]")
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object) {BaseAddress = new Uri("http://anyoldurl/")};
            var sut = new BpdtsTestAppService(httpClient);

            var result = await sut.GetUsers();

            result.ShouldNotBeNull();
        }
    }
}
