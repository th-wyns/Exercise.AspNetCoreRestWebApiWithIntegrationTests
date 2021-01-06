using System.ComponentModel.DataAnnotations;

namespace Users.Models.DTOs
{
    /// <summary>
    /// User resource creation model.
    /// </summary>
    public class UserCreateDto
    {
        /// <summary>
        /// Sets the user's name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Sets the user's username.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Sets the user's email address.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Sets the user's address.
        /// </summary>
        public AddressDto Address { get; set; }
        /// <summary>
        /// Sets the user's phone number.
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Sets the user's website.
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// Sets the user's company.
        /// </summary>
        public CompanyDto Company { get; set; }
    }
}
