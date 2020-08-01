using PeopleLocationApi.Constants;
using PeopleLocationApi.Helpers;
using PeopleLocationApi.Models;

namespace PeopleLocationApi.Predicates
{
    public class UserPredicate
    {
        public static bool IsWithin(User user, double miles)
        {
            return GeoCalculator.GetDistanceInMiles(LondonCityConstants.Latitude,
                LondonCityConstants.Longitude,
                user.Latitude, user.Longitude) <= miles;
        }
    }
}
