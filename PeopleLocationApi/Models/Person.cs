namespace PeopleLocationApi.Models
{
    public class Person
    {
        public long Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public static Person Create(long id, string firstName, string lastName, string email)
        {
            return new Person
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
        }
    }
}