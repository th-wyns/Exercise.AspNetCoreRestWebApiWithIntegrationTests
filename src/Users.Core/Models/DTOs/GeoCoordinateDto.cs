namespace Users.Models.DTOs
{
    /// <summary>
    /// GPS coordinate representation.
    /// </summary>
    public class GeoCoordinateDto
    {
        /// <summary>
        /// The latitude coordinate.
        /// </summary>
        public double Lat { get; set; }
        /// <summary>
        /// The longitude coordinate.
        /// </summary>
        public double Lng { get; set; }
    }
}
