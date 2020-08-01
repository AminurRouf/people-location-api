using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PeopleLocationApi.Helpers;
using PeopleLocationApi.Models;

namespace PeopleLocationApi.Services
{
    public interface IBpdtsTestAppService
    {
        Task<IEnumerable<Person>> GetPeopleLivingInLondon();
    }

    public class BpdtsTestAppService : IBpdtsTestAppService
    {
        private readonly HttpClient _httpClient;

        public BpdtsTestAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Person>> GetPeopleLivingInLondon()
        {
            var response = await _httpClient.GetAsync("city/London/users");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStringAsync();
            return JsonConvertorHelper.DeserializeContent<IEnumerable<Person>>(responseStream);
        }
    }
}