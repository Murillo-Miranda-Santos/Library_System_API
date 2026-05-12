using LibrarySystemAPI.Models;
namespace LibrarySystemAPI.Services;

public class UserService
{
    private readonly LibraryContext _context;

    public UserService(LibraryContext context)
    {
        _context = context;
    }

    public List<User>? GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public User? GetUser(int id)
    {
        return _context.Users.FirstOrDefault(x => x.Id == id);
    }

    public User? PostUser(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Name))
            return null;

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public User? PutUser(int id, User updatedUser)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id);

        if (user == null) 
            return null;

        var lastUser = new User
        {
            Id = user.Id,
            Name = user.Name,
        };

        user.Name = updatedUser.Name;
        _context.SaveChanges();

        return user;
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
