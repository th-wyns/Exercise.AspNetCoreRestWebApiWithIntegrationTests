namespace Users.Models.DTOs
{
    /// <summary>
    /// The User resource.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The identifier of the user.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The username of the user.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// The email address of the user.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The address of the user.
        /// </summary>
        public AddressDto Address { get; set; }
        /// <summary>
        /// The phone of the user.
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// The website of the user.
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// The company of the user.
        /// </summary>
        public CompanyDto Company { get; set; }
    }
}
