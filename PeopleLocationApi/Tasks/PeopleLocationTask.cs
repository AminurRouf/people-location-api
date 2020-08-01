using System.Collections.Generic;
using System.Threading.Tasks;
using PeopleLocationApi.Constants;
using PeopleLocationApi.Models;
using PeopleLocationApi.Predicates;
using PeopleLocationApi.Services;

namespace PeopleLocationApi.Tasks
{
    public interface IPeopleLocationTask
    {
        Task<IEnumerable<Person>> GetPeopleLivingIn(string city);
        Task<IEnumerable<Person>> GetPeopleLivingWithIn(string city, double miles);
    }

    public class PeopleLocationTask : IPeopleLocationTask
    {
        private readonly IBpdtsTestAppService _bpdtsTestAppService;

        public PeopleLocationTask(IBpdtsTestAppService bpdtsTestAppService)
        {
            _bpdtsTestAppService = bpdtsTestAppService;
        }

        public Task<IEnumerable<Person>> GetPeopleLivingIn(string city)
        {
            return _bpdtsTestAppService.GetPeopleLivingIn(LondonCityConstants.Name);
        }

        public async Task<IEnumerable<Person>> GetPeopleLivingWithIn(string city, double miles)
        {
            var users = await _bpdtsTestAppService.GetUsers();
            return FilterWithIn(users, miles);
        }

        private static List<Person> FilterWithIn(List<User> users, double miles)
        {
            return new List<Person>(users?.FindAll(user => UserPredicate.IsWithin(user, miles)));
        }
    }
}