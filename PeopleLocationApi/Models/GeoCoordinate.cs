namespace PeopleLocationApi.Models
{
    public struct GeoCoordinate
    {
        public double Latitude { get; }

        public double Longitude { get; }

        public GeoCoordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", Latitude, Longitude);
        }


    }

}
