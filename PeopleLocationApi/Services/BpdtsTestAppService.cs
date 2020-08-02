using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PeopleLocationApi.Helpers;
using PeopleLocationApi.Models;

namespace PeopleLocationApi.Services
{
    public interface IBpdtsTestAppService
    {
        Task<IEnumerable<Person>> GetPeopleLivingIn(string city);
        Task<List<User>> GetUsers();
    }
    /// <summary>
    /// This service encapsulates the calls the bpdts test app endpoints using a httpClient
    /// and returns the deserialised response as lists of People (or its sub type Users)
    /// </summary>
    public class BpdtsTestAppService : IBpdtsTestAppService
    {
        private readonly HttpClient _httpClient;

        public BpdtsTestAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Person>> GetPeopleLivingIn(string city)
        {
            var response = await _httpClient.GetAsync($"city/{city}/users");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStringAsync();
            return JsonConvertorHelper.DeserializeContent<List<Person>>(responseStream);
        }

        public async Task<List<User>> GetUsers()
        {
            var response = await _httpClient.GetAsync($"users");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStringAsync();
            return JsonConvertorHelper.DeserializeContent<List<User>>(responseStream);
        }
    }
}