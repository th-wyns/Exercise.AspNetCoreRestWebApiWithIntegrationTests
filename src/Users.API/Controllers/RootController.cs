using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Users.Models.DTOs;

namespace Users.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        /// <summary>
        /// API root.
        /// </summary>
        /// <returns>HATEOAS links for top resources.</returns>
        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot()
        {
            var links = new List<LinkDto>() {
                new LinkDto(Url.Link("GetRoot", new { }), "self", "GET"),
                new LinkDto(Url.Link("GetUsers", new { }), "users", "GET"),
                new LinkDto(Url.Link("CreateUser", new { }), "create_user", "POST")
            };
            return Ok(links);

        }

    }
}
