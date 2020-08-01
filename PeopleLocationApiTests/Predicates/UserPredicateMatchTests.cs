using PeopleLocationApi.Models;
using PeopleLocationApi.Predicates;
using PeopleLocationApiTests.TestData;
using Xunit;

namespace PeopleLocationApiTests.Predicates
{
    public class UserPredicateTest
    {
        [Theory]
        [MemberData(nameof(PeopleTestData.GetUsersWithinFiftyMilesOfLondon), MemberType = typeof(PeopleTestData))]
        public void AllUsersShouldBeWithInFiftyMilesOfLondon(User user1, User user2, User user3)
        {
            Assert.True(UserPredicate.IsWithin(user1, 50));
            Assert.True(UserPredicate.IsWithin(user2, 50));
            Assert.True(UserPredicate.IsWithin(user3, 50));
        }

        [Theory]
        [MemberData(nameof(PeopleTestData.GetUsersMoreThanFiftyMilesFromLondon), MemberType = typeof(PeopleTestData))]
        public void AllUsersShouldBeMoreThanFiftyMilesFromLondon(User user1, User user2)
        {
            Assert.False(UserPredicate.IsWithin(user1, 50));
            Assert.False(UserPredicate.IsWithin(user2, 50));
        }

   
    }
}