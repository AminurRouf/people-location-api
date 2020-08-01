using System.Collections.Generic;
using PeopleLocationApi.Models;

namespace PeopleLocationApiTests.TestData
{
    public class PeopleTestData
    {
        public static List<Person> GetPeople()
        {
            return new List<Person> {Person.Create(1, "firstName1", "lastName1", "test@me.com")};
        }

        public static List<User> GetUsers()
        {
            return new List<User>
            {
                User.Create(1, "firstName1", "lastName1", "test@me.com", 51.5128, -0.09184,
                    "1"),
                User.Create(2, "firstName2", "lastName2", "test@me.com", 0, 0,
                    "2")
            };
        }

        public static IEnumerable<object[]> GetUsersWithinFiftyMilesOfLondon()
        {
            yield return new object[]
            {
                User.Create(1, "firstName1", "lastName1", "test@me.com", 51.5128, -0.09184,
                    "1"), //Coordinates for London
                User.Create(2, "firstName2", "lastName2", "test@me.com", 51.519539, -0.126960,
                    "2"), //Coordinates for British Museum
                User.Create(3, "firstName3", "lastName3", "test@me.com", 51.536280, -0.153280,
                    "3"), //Coordinates for ZSL London Zoo
              
            };
        }
        public static IEnumerable<object[]> GetUsersMoreThanFiftyMilesFromLondon()
        {
            yield return new object[]
            {
                User.Create(4, "firstName4", "lastName4", "test@me.com", 55.008037, -1.590167,
                    "4"), //Coordinates for HMRC Benton Park View
                User.Create(5, "firstName5", "lastName5", "test@me.com", 55.008037, -1.590167,
                    "5") //Coordinates for Statue Of Liberty, New York, USA
            };
        }
    }
}