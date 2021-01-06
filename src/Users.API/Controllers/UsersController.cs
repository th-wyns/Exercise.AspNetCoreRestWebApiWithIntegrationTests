using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Models.DTOs;
using Users.Models.Entities;
using Users.Repositories;

namespace Users.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets the user resources.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            var result = _mapper.Map<List<UserDto>>(users);
            var links = CreateLinksForUsers();
            var resultWithLinks = new UsersWithLinksDto(result, links);
            return Ok(resultWithLinks);
        }

        /// <summary>
        /// Gets the user with the specified identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public async Task<ActionResult<UserWithLinksDto>> GetUser(string id)
        {
            var user = await _userRepository.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<UserWithLinksDto>(user);
            result.Links = CreateLinksForUser(id);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userCreateDto">The user creation model.</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<UserDto>> CreateUser(UserCreateDto userCreateDto)
        {
            var userToCreate = _mapper.Map<User>(userCreateDto);
            var createdUser = await _userRepository.CreateUserAsync(userToCreate);
            var result = _mapper.Map<UserDto>(createdUser);
            return CreatedAtRoute("GetUser", new { id = createdUser.Id.ToString() }, result);
        }

        /// <summary>
        /// Updates the user with the specified identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <param name="userCreateDto">The user update model.</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:length(24)}", Name = "UpdateUser")]
        public async Task<ActionResult<UserDto>> UpdateUser(string id, UserCreateDto userCreateDto)
        {
            var originalUser = await _userRepository.GetUserAsync(id);

            if (originalUser == null)
            {
                return NotFound();
            }

            var userToUpdate = _mapper.Map(userCreateDto, originalUser);

            try
            {
                await _userRepository.UpdateUserAsync(id, userToUpdate);
            }
            catch (RepositoryException rex)
            {
                if (rex.ErrorType == ErrorType.NoMatch)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Patches the user with the specified identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <param name="userPatch">The user patch model.</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id:length(24)}", Name = "PatchUser")]
        public async Task<ActionResult<UserDto>> PatchUser(string id, JsonPatchDocument<UserUpdateDto> userPatch)
        {
            var user = await _userRepository.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userPatchDto = _mapper.Map<UserUpdateDto>(user);
            userPatch.ApplyTo(userPatchDto, ModelState);

            if (!TryValidateModel(userPatchDto))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(userPatchDto, user);

            try
            {
                await _userRepository.UpdateUserAsync(id, user);
            }
            catch (RepositoryException rex)
            {
                if (rex.ErrorType == ErrorType.NoMatch)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes the user with the specified identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:length(24)}", Name = "DeleteUser")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _userRepository.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                await _userRepository.RemoveUserAsync(user);
            }
            catch (RepositoryException rex)
            {
                if (rex.ErrorType == ErrorType.NoMatch)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        List<LinkDto> CreateLinksForUser(string id)
        {
            var links = new List<LinkDto>();
            links.Add(new LinkDto(Url.Link("GetUser", new { id }), "self", "GET"));
            links.Add(new LinkDto(Url.Link("DeleteUser", new { id }), "delete_user", "DELETE"));
            links.Add(new LinkDto(Url.Link("UpdateUser", new { id }), "update_user", "POST"));
            links.Add(new LinkDto(Url.Link("PatchUser", new { id }), "patch_user", "PATCH"));
            return links;
        }

        List<LinkDto> CreateLinksForUsers()
        {
            var links = new List<LinkDto>();
            links.Add(new LinkDto(Url.Link("GetUsers", null), "self", "GET"));
            return links;
        }


        //[HttpPost("{id:length(24)}")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //public async Task<ActionResult<User>> CreateUser(string id, UserCreateDto userCreateDto)
        //{
        //    var user = await _userRepository.GetUserAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Conflict();
        //}

        //[HttpOptions]
        //public IActionResult GetUsersOptions()
        //{
        //    Response.Headers.Add("Allow", "GET,HEAD,POST,OPTONS");
        //    return Ok();
        //}
    }
}
