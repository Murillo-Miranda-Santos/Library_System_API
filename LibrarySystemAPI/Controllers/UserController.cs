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
        public async Task<IActionResult> Get()
        {
            var UserResponseDto = await _userService.GetAllUsers();

            return Ok(UserResponseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var UserResponseDto = await _userService.GetUser(id);

            if (UserResponseDto == null)
                return NotFound("User not found.");

            return Ok(UserResponseDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserDto createUserDto)
        {
            var userResponseDto = await _userService.PostUser(createUserDto); 

            return Ok(userResponseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateUserDto updateUserDto)
        {
            var userResponseDto = await _userService.PutUser(id, updateUserDto);

            if (userResponseDto == null)
                return NotFound("User not found.");

            return Ok(userResponseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteUser(id);

            if (result.Success == false)
                return NotFound(result.Message);

            return NoContent();
        }
    }
}