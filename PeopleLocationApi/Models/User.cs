namespace PeopleLocationApi.Models
{
    public class User : Person
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string IpAddress { get; private set; }

        public static User Create(long id, string firstName, string lastName, string email, double latitude,
            double  longitude, string ipAddress)
        {
            return new User
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Latitude = latitude,
                Longitude = longitude,
                IpAddress = ipAddress
            };
        }
    }
}