﻿using System.Linq;
using Moq;
using PeopleLocationApi.Constants;
using PeopleLocationApi.Models;
using PeopleLocationApi.Services;
using PeopleLocationApi.Tasks;
using PeopleLocationApiTests.TestData;
using Shouldly;
using Xunit;

namespace PeopleLocationApiTests.Tasks
{
    public class PeopleLocationTaskTests
    {
        [Fact]
        public async void PeopleLivingInAPlaceShouldReturnPeople()
        {
            // Arrange
            var people = PeopleTestData.GetPeople();
            var mock = new Mock<IBpdtsTestAppService>();
            mock.Setup(x => x.GetPeopleLivingIn(LondonCityConstants.Name)).ReturnsAsync(people).Verifiable();
            var sut = new PeopleLocationTask(mock.Object);

            //Act
            var result = await sut.GetPeopleLivingIn(LondonCityConstants.Name);

            //Assert
            mock.Verify(x => x.GetPeopleLivingIn(LondonCityConstants.Name), Times.Once);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(people.Count);
            result.ShouldBe(people);
        }

        [Fact]
        public async void UsersWithinFiftyMilesOfAPlaceShouldReturnPeople()
        {
            // Arrange
            const double miles = 50;
            var users = PeopleTestData.GetUsers();
            var people = PeopleTestData.GetPeople();
            var mock = new Mock<IBpdtsTestAppService>();
            mock.Setup(x => x.GetUsers()).ReturnsAsync(users).Verifiable();
            var sut = new PeopleLocationTask(mock.Object);

            //Act
            var result = await sut.GetPeopleCoordinatesWithIn(miles,
                new GeoCoordinate(LondonCityConstants.Latitude, LondonCityConstants.Longitude));

            //Assert
            mock.Verify(x => x.GetUsers(), Times.Once);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(people.Count);
        }

        [Fact]
        public async void UsersEitherLivingInOrWithinFiftyMilesPlaceShouldReturnDistinctPeople()
        {
            // Arrange
            const double miles = 50;
            var users = PeopleTestData.GetUsers();
            var people = PeopleTestData.GetPeople();
            var expectedCount = 1; // Cause first person in users and people are the same
            var mock = new Mock<IBpdtsTestAppService>();
            mock.Setup(x => x.GetUsers()).ReturnsAsync(users).Verifiable();
            mock.Setup(x => x.GetPeopleLivingIn(LondonCityConstants.Name)).ReturnsAsync(people).Verifiable();
            var sut = new PeopleLocationTask(mock.Object);

            //Act
            var result = await sut.GetPeopleLivingInOrCoordinatesWithInFiftyMiles(LondonCityConstants.Name, miles,
                new GeoCoordinate(LondonCityConstants.Latitude, LondonCityConstants.Longitude));

            //Assert
            mock.Verify(x => x.GetUsers(), Times.Once);
            mock.Verify(x => x.GetPeopleLivingIn(LondonCityConstants.Name), Times.Once);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(expectedCount);
        }
    }
}