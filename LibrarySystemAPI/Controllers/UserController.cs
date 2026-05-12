using LibrarySystemAPI.Models;
using LibrarySystemAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystemAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userService.GetUser(id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            var created = _userService.PostUser(user);

            if (created == null)
                return BadRequest("Invalid user data.");

            return Ok(created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, User updatedUser)
        {
            var user = _userService.PutUser(id, updatedUser);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _userService.DeleteUser(id);

            if (result == null)
                return NotFound("User not found.");

            if (result == false)
                return BadRequest("User has active loans.");

            return NoContent();
        }
    }
}