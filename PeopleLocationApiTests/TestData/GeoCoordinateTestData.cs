using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeopleLocationApi.Constants;
using PeopleLocationApi.Models;

namespace PeopleLocationApiTests.TestData
{
    public class GeoCoordinateTestData
    {
        public static IEnumerable<object[]> GetSameCoordinatesData()
        {
            var coordinates = new List<object[]>
            {
                new object[]
                {
                    new GeoCoordinate(LondonCityConstants.Latitude, LondonCityConstants.Longitude),
                    new GeoCoordinate(LondonCityConstants.Latitude, LondonCityConstants.Longitude)
                },
                new object[]
                {
                    new GeoCoordinate(55.008037, -1.590167),
                    new GeoCoordinate(55.008037, -1.590167)
                }
            };

            return coordinates;
        }

        public static IEnumerable<object[]> GetCoordinatesWithinFiftyMilesData()
        {
            var coordinates = new List<object[]>
            {
                new object[] 
                {
                    new GeoCoordinate(LondonCityConstants.Latitude, LondonCityConstants.Longitude),
                    new GeoCoordinate(LondonCityConstants.Latitude, LondonCityConstants.Longitude)
                },
                new object[]
                {
                    //British Museum
                    new GeoCoordinate(LondonCityConstants.Latitude, LondonCityConstants.Longitude),
                    new GeoCoordinate(51.519539, -0.126960)
                },
                new object[]
                {
                    //ZSL London Zoo
                    new GeoCoordinate(LondonCityConstants.Latitude, LondonCityConstants.Longitude),
                    new GeoCoordinate(51.536280, -0.153280)
                }
            };

            return coordinates;
        }

        public static IEnumerable<object[]> GetCoordinatesMoreThanFiftyMilesData()
        {
            var coordinates = new List<object[]>
            {
                new object[]
                {
                    //HMRC Benton Park View
                    new GeoCoordinate(LondonCityConstants.Latitude, LondonCityConstants.Longitude),
                    new GeoCoordinate(55.008037, -1.590167)
                },
                new object[]
                {
                    //Statue Of Liberty, New York, USA
                    new GeoCoordinate(40.689382, -74.044109),
                    new GeoCoordinate(51.536280, -0.153280)
                }
            };

            return coordinates;
        }
    }
}
