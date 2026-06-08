using LibrarySystemAPI.Common;
using LibrarySystemAPI.DTOs.users;
using LibrarySystemAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace LibrarySystemAPI.Services;

public class UserService
{
    private readonly LibraryContext _context;

    public UserService(LibraryContext context)
    {
        _context = context;
    }

    public async Task<List<UserResponseDto>> GetAllUsers()
    {
        List<UserResponseDto> userResponseDtos = new List<UserResponseDto>();

        var users = await _context.Users.ToListAsync();

        foreach (var user in users)
        {
            UserResponseDto dto = new()
            {
                Id = user.Id,
                Name = user.Name
            };

            userResponseDtos.Add(dto);
        }

        return userResponseDtos;
    }

    public async Task<UserResponseDto?> GetUser(int id)
    {
        var book = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (book == null)
            return null;

        UserResponseDto userResponseDto = new()
        {
            Id = book.Id,
            Name = book.Name
        };

        return userResponseDto;
    }

    public async Task<UserResponseDto?> PostUser(CreateUserDto createUserDto)
    {
        User user = new()
        {
            Name = createUserDto.Name,
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        UserResponseDto userResponseDto = new()
        {
            Id = user.Id,
            Name = user.Name,
        };

        return userResponseDto;
    }

    public async Task<UserResponseDto?> PutUser(int id, UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null) 
            return null;

        var lastUser = new User()
        {
            Id = user.Id,
            Name = user.Name,
        };

        user.Name = updateUserDto.Name;
        await _context.SaveChangesAsync();

        UserResponseDto userResponseDto = new()
        {
            Id = user.Id,
            Name= user.Name
        };

        return userResponseDto;
    }

    public async Task<ServiceResult<User>> DeleteUser(int id)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id);

        if (user == null)
        {
            return new ServiceResult<User>()
            {
                Success = false,
                Message = "Usuario não encontrado."
            };
        }
            

        var hasLoan = await _context.Loans.AnyAsync(x =>
        x.UserId == id && x.ReturnDate == null);

        if (hasLoan == true)
        {
            return new ServiceResult<User>()
            {
                Success = false,
                Message = "Usuario possuí empréstimos ativos."
            };
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return new ServiceResult<User>()
        {
            Success = true,
            Data = user
        };
    }
}
