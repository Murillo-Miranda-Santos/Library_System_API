using LibrarySystemAPI.DTOs.users;
using LibrarySystemAPI.Models;
namespace LibrarySystemAPI.Services;

public class UserService
{
    private readonly LibraryContext _context;

    public UserService(LibraryContext context)
    {
        _context = context;
    }

    public List<UserResponseDto> GetAllUsers()
    {
        List<UserResponseDto> userResponseDtos = new List<UserResponseDto>();

        foreach (var user in _context.Users)
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

    public UserResponseDto? GetUser(int id)
    {
        var book = _context.Users.FirstOrDefault(x => x.Id == id);

        if (book == null)
            return null;

        UserResponseDto userResponseDto = new()
        {
            Id = book.Id,
            Name = book.Name
        };

        return userResponseDto;
    }

    public UserResponseDto? PostUser(CreateUserDto createUserDto)
    {
        User user = new()
        {
            Name = createUserDto.Name,
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        UserResponseDto userResponseDto = new()
        {
            Id = user.Id,
            Name = user.Name,
        };

        return userResponseDto;
    }

    public UserResponseDto? PutUser(int id, UpdateUserDto updateUserDto)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id);

        if (user == null) 
            return null;

        var lastUser = new User()
        {
            Id = user.Id,
            Name = user.Name,
        };

        user.Name = updateUserDto.Name;
        _context.SaveChanges();

        UserResponseDto userResponseDto = new()
        {
            Id = user.Id,
            Name= user.Name
        };

        return userResponseDto;
    }

    public bool? DeleteUser(int id)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id);

        if (user == null)
            return null;

        var hasLoan = _context.Loans.Any(x =>
        x.UserId == id && x.ReturnDate == null);

        if (hasLoan == true) 
             return false;

        _context.Users.Remove(user);
        _context.SaveChanges();

        return true;
    }
}
