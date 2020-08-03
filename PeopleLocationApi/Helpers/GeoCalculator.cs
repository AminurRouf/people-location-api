using System;
using PeopleLocationApi.Constants;
using PeopleLocationApi.Models;

namespace PeopleLocationApi.Helpers
{
    /// <summary>
    /// Utility methods for calculating distance between two sets of geo coordinates.
    /// </summary>
    public class GeoCalculator
    {
        /// <summary>
        /// Calculate the distance between two sets of coordinates.
        /// The GeoCoordinate class is not available in .NET core. So looked at  the source code in .NET 4.8
        /// and using various sources (stackoverflow!) came up with this  version using the Haversine formula according to Dr. Math.
        ///  http://mathforum.org/library/drmath/view/51879.html
        /// <param name="source">GeoCoordinate of source</param>
        /// <param name="destination">GeoCoordinate of destination</param>
        /// <returns>A <see cref="Double"/> value representing the distance in miles from the origin to the destination coordinate</returns>
        /// </summary>
        public static double GetDistanceInMiles(GeoCoordinate source, GeoCoordinate destination)
        {
            if (source.Longitude == destination.Longitude && source.Latitude == destination.Latitude)
                return 0;

            var d1 = source.Latitude * (Math.PI / 180.0);
            var num1 = source.Longitude * (Math.PI / 180.0);


            var d2 = destination.Latitude * (Math.PI / 180.0);
            var num2 =  destination.Longitude * (Math.PI / 180.0) - num1;


            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            //  circle distance in Radians.
            double radians = 2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3));

            const double earthRadiusInMiles = 3956.0;
            return earthRadiusInMiles * radians;
        }

        public static bool IsWithinDistance(GeoCoordinate source, GeoCoordinate destination, double miles)
        {
            return GetDistanceInMiles(source, destination) <= miles;
        }

    }
}