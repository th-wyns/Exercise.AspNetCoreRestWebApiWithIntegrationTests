namespace Users.Models.DTOs
{
    /// <summary>
    /// Address representation.
    /// </summary>
    public class AddressDto
    {
        /// <summary>
        /// The street of the address.
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// The suite of the address.
        /// </summary>
        public string Suite { get; set; }
        /// <summary>
        /// The city of the address.
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// The zip code of the address.
        /// </summary>
        public string Zipcode { get; set; }
        /// <summary>
        /// The GPS coordinates of the address.
        /// </summary>
        public GeoCoordinateDto Geo { get; set; }
    }
}
