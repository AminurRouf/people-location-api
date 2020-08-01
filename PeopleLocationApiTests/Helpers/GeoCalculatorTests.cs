using PeopleLocationApi.Constants;
using PeopleLocationApi.Helpers;
using Shouldly;
using Xunit;

namespace PeopleLocationApiTests.Helpers
{
    public class GeoCalculatorTests
    {
        [Theory]
        [InlineData(LondonCityConstants.Latitude, LondonCityConstants.Longitude, LondonCityConstants.Latitude,
            LondonCityConstants.Longitude, 0)] //London according to latlong.net
        public void ShouldBeZeroMiles(double originLatitude, double originLongitude, double destinationLatitude,
            double destinationLongitude, double expected)
        {
            var result = GeoCalculator.GetDistanceInMiles(originLatitude, originLongitude, destinationLatitude,
                destinationLongitude);
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData(LondonCityConstants.Latitude, LondonCityConstants.Longitude, 51.519539, -0.126960, 50)] //British Museum
        [InlineData(LondonCityConstants.Latitude, LondonCityConstants.Longitude, 51.536280, -0.153280, 50)] //ZSL London Zoo
        public void ShouldBeLessThanOrEqualTo(double originLatitude, double originLongitude, double destinationLatitude,
            double destinationLongitude, double expected)
        {
            var result = GeoCalculator.GetDistanceInMiles(originLatitude, originLongitude, destinationLatitude,
                destinationLongitude);
            result.ShouldBeLessThanOrEqualTo(expected);
        }

        [Theory]
        [InlineData(LondonCityConstants.Latitude, LondonCityConstants.Longitude, 55.008037, -1.590167, 50)] //HMRC Benton Park View
        [InlineData(LondonCityConstants.Latitude, LondonCityConstants.Longitude, 40.689382, -74.044109, 50)] //Statue Of Liberty, New York, USA
        public void ShouldBeGreaterThan(double originLatitude, double originLongitude, double destinationLatitude,
            double destinationLongitude, double expected)
        {
            var result = GeoCalculator.GetDistanceInMiles(originLatitude, originLongitude, destinationLatitude,
                destinationLongitude);
            result.ShouldBeGreaterThan(expected);
        }
    }
}