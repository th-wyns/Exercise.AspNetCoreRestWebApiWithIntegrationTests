using System.Collections.Generic;

namespace Users.Models.DTOs
{
    /// <summary>
    /// User resources with HATEOAS links.
    /// </summary>
    /// <seealso cref="Users.Models.DTOs.UserDto" />
    public class UsersWithLinksDto
    {
        /// <summary>
        /// The users.
        /// </summary>
        public List<UserDto> Value { get; set; }
        /// <summary>
        /// The HATEOAS links.
        /// </summary>
        public List<LinkDto> Links { get; set; }

        public UsersWithLinksDto()
        {
        }

        public UsersWithLinksDto(List<UserDto> value, List<LinkDto> links)
        {
            Value = value;
            Links = links;
        }
    }
}
