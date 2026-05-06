using LibrarySystemAPI.Models;
using LibrarySystemAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystemAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService = new UserService();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = userService.GetUser(id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            var created = userService.PostUser(user);

            if (created == null)
                return BadRequest("Invalid user data.");

            return Ok(created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, User updatedUser)
        {
            var user = userService.PutUser(id, updatedUser);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = userService.DeleteUser(id);

            if (result == null)
                return NotFound("User not found.");

            if (result == false)
                return BadRequest("User has active loans.");

            return NoContent();
        }
    }
}