using System.Collections.Generic;

namespace Users.Models.DTOs
{
    /// <summary>
    /// User resource with HATEOAS links.
    /// </summary>
    /// <seealso cref="Users.Models.DTOs.UserDto" />
    public class UserWithLinksDto : UserDto
    {
        /// <summary>
        /// The HATEOAS links.
        /// </summary>
        public List<LinkDto> Links { get; set; }
    }
}
