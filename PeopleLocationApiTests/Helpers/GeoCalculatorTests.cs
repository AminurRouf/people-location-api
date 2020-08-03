using PeopleLocationApi.Helpers;
using PeopleLocationApi.Models;
using PeopleLocationApiTests.TestData;
using Shouldly;
using Xunit;

namespace PeopleLocationApiTests.Helpers
{
    public class GeoCalculatorTests
    {
        [Theory]
        [MemberData(nameof(GeoCoordinateTestData.GetSameCoordinatesData),  MemberType = typeof(GeoCoordinateTestData))]
        public void ShouldBeZeroMiles(GeoCoordinate source,GeoCoordinate destination)
        {
            var result = GeoCalculator.GetDistanceInMiles(source, destination);
            result.ShouldBe(0);
        }

        [Theory]
        [MemberData(nameof(GeoCoordinateTestData.GetCoordinatesWithinFiftyMilesData),  MemberType = typeof(GeoCoordinateTestData))]
        public void ShouldBeLessThanOrEqualTo(GeoCoordinate source,GeoCoordinate destination)
        {
            var result = GeoCalculator.GetDistanceInMiles(source, destination);
            result.ShouldBeLessThanOrEqualTo(50);
        }

        [Theory]
        [MemberData(nameof(GeoCoordinateTestData.GetCoordinatesMoreThanFiftyMilesData),  MemberType = typeof(GeoCoordinateTestData))]
        public void ShouldBeGreaterThan(GeoCoordinate source,GeoCoordinate destination)
        {
            var result = GeoCalculator.GetDistanceInMiles(source, destination);
            result.ShouldBeGreaterThan(50);
        }

        [Theory]
        [MemberData(nameof(GeoCoordinateTestData.GetCoordinatesWithinFiftyMilesData), MemberType = typeof(GeoCoordinateTestData))]
        public void ShouldBeWithInFiftyMiles(GeoCoordinate source, GeoCoordinate destination)
        {
            var result = GeoCalculator.IsWithinDistance(source, destination, 50);
            result.ShouldBe(true);
        }

        [Theory]
        [MemberData(nameof(GeoCoordinateTestData.GetCoordinatesMoreThanFiftyMilesData), MemberType = typeof(GeoCoordinateTestData))]
        public void ShouldBeMoreThanFiftyMiles(GeoCoordinate source, GeoCoordinate destination)
        {
            var result = GeoCalculator.IsWithinDistance(source, destination, 50);
            result.ShouldBe(false);
        }


    }
}