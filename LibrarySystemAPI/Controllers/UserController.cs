using LibrarySystemAPI.DTOs.users;
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
            var UserResponseDto = _userService.GetAllUsers();

            return Ok(UserResponseDto);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var UserResponseDto = _userService.GetUser(id);

            if (UserResponseDto == null)
                return NotFound("User not found.");

            return Ok(UserResponseDto);
        }

        [HttpPost]
        public IActionResult Post(CreateUserDto createUserDto)
        {
            var userResponseDto = _userService.PostUser(createUserDto); 

            return Ok(userResponseDto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateUserDto updateUserDto)
        {
            var userResponseDto = _userService.PutUser(id, updateUserDto);

            if (userResponseDto == null)
                return NotFound("User not found.");

            return Ok(userResponseDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userResponseDto = _userService.DeleteUser(id);

            if (userResponseDto == null)
                return NotFound("User not found.");

            if (userResponseDto == false)
                return BadRequest("User has active loans.");

            return NoContent();
        }
    }
}