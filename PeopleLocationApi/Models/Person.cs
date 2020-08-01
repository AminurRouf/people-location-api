namespace PeopleLocationApi.Models
{
    public class Person
    {
        public long Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }

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