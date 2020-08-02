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
            var usersWithin =  await GetPeopleCoordinatesWithIn(city, miles);

            //Concat the two lists then do a groupby on id to get a distinct list of people.
            //A union will not work as users are a sub type of people, the extra properties
            //on user will mean the same person will be treated as different person on the join.
            var people = peopleLivingIn.Concat(usersWithin);

            var distinctPeople = people.GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToList();

            return distinctPeople;
        }

        private static List<Person> FilterWithIn(List<User> users, double miles)
        {
            return new List<Person>(users?.FindAll(user => UserPredicate.IsWithin(user, miles)));
        }
    }
}