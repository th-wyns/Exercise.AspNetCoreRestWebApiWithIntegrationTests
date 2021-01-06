namespace Users.Models.Entities
{
    public class Address
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public GeoCoordinate Geo { get; set; }
    }
}
