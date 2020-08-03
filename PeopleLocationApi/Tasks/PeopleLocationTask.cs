using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeopleLocationApi.Helpers;
using PeopleLocationApi.Models;
using PeopleLocationApi.Services;

namespace PeopleLocationApi.Tasks
{
    public interface IPeopleLocationTask
    {
        Task<IEnumerable<Person>> GetPeopleLivingIn(string place);
        Task<IEnumerable<Person>> GetPeopleCoordinatesWithIn(double miles, GeoCoordinate coordinate);
        Task<IEnumerable<Person>> GetPeopleLivingInOrCoordinatesWithInFiftyMiles(string place, double miles, GeoCoordinate coordinate);
    }

    /// <summary>
    /// The purpose of this class is to coordinate the interaction of the client
    /// with the business functionality of getting users form the bpdts test app
    /// and returning filtered people. 
    /// </summary>
    public class PeopleLocationTask : IPeopleLocationTask
    {
        private readonly IBpdtsTestAppService _bpdtsTestAppService;

        public PeopleLocationTask(IBpdtsTestAppService bpdtsTestAppService)
        {
            _bpdtsTestAppService = bpdtsTestAppService;
        }

        public async Task<IEnumerable<Person>> GetPeopleLivingIn(string place)
        {
            return await _bpdtsTestAppService.GetPeopleLivingIn(place);
        }

        public async Task<IEnumerable<Person>> GetPeopleCoordinatesWithIn(double miles, GeoCoordinate coordinate)
        {
            var users = await _bpdtsTestAppService.GetUsers();
            return FilterWithIn(coordinate, users, miles);
        }

        public async Task<IEnumerable<Person>> GetPeopleLivingInOrCoordinatesWithInFiftyMiles(string place, double miles, GeoCoordinate coordinate)
        {
            List<Person> peopleLivingIn = (List<Person>) await GetPeopleLivingIn(place);
            var usersWithin = await GetPeopleCoordinatesWithIn(miles, coordinate );

            //Concat the two lists then do a groupby on id to get a distinct list of people.
            //A union or distinct will not work as users are a sub type of people, the extra properties
            //on user will mean the same person will be treated as different person on the join.
            var people = peopleLivingIn.Concat(usersWithin);

            var distinctPeople = people.GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToList();

            return distinctPeople;
        }

        private static List<Person> FilterWithIn(GeoCoordinate sourceCoordinate, List<User> users, double miles)
        {
            return new List<Person>(users?.FindAll(user =>
                GeoCalculator.IsWithinDistance(sourceCoordinate, new GeoCoordinate(user.Latitude, user.Longitude), miles)));
        }
    }
}