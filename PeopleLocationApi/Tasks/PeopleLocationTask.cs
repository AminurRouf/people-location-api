using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeopleLocationApi.Models;
using PeopleLocationApi.Predicates;
using PeopleLocationApi.Services;

namespace PeopleLocationApi.Tasks
{
    public interface IPeopleLocationTask
    {
        Task<IEnumerable<Person>> GetPeopleLivingIn(string city);
        Task<IEnumerable<Person>> GetPeopleCoordinatesWithIn(string city, double miles);
        Task<IEnumerable<Person>> GetPeopleLivingInOrCoordinatesWithInFiftyMiles(string london, double miles);
    }

    public class PeopleLocationTask : IPeopleLocationTask
    {
        private readonly IBpdtsTestAppService _bpdtsTestAppService;

        public PeopleLocationTask(IBpdtsTestAppService bpdtsTestAppService)
        {
            _bpdtsTestAppService = bpdtsTestAppService;
        }

        public async Task<IEnumerable<Person>> GetPeopleLivingIn(string city)
        {
            return await _bpdtsTestAppService.GetPeopleLivingIn(city);
        }

        public async Task<IEnumerable<Person>> GetPeopleCoordinatesWithIn(string city, double miles)
        {
            var users = await _bpdtsTestAppService.GetUsers();
            return FilterWithIn(users, miles);
        }

        public async Task<IEnumerable<Person>> GetPeopleLivingInOrCoordinatesWithInFiftyMiles(string city, double miles)
        {
            List<Person> peopleLivingIn = (List<Person>) await GetPeopleLivingIn(city);
            var peopleWithin = await GetPeopleCoordinatesWithIn(city, miles);
            var people = peopleLivingIn.Union(peopleWithin).ToList();
            return people;
        }

        private static List<Person> FilterWithIn(List<User> users, double miles)
        {
            return new List<Person>(users?.FindAll(user => UserPredicate.IsWithin(user, miles)));
        }
    }
}