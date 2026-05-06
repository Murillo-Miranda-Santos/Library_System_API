using LibrarySystemAPI.Models;
namespace LibrarySystemAPI.Services;

public class UserService
{
    public List<User>? GetAllUsers()
    {
        var users = DataStore.users;

        return users;
    }

    public User? GetUser(int id)
    {
        var user = DataStore.users.FirstOrDefault(x => x.Id == id);

        return user;
    }

    public User? PostUser(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Name))
            return null;

        user.Id = DataStore.users.Any() ? DataStore.users.Max(x => x.Id) + 1 : 1;

        DataStore.users.Add(user);

        return user;
    }

    public User? PutUser(int id, User updatedUser)
    {
        var user = DataStore.users.FirstOrDefault(x => x.Id == id);

        if (user == null) 
            return null;

        var lastUser = new User
        {
            Id = user.Id,
            Name = user.Name,
        };

        user.Name = updatedUser.Name;

        return user;
    }

    public bool? DeleteUser(int id)
    {
        var user = DataStore.users.FirstOrDefault(x => x.Id == id);

        if (user == null)
            return null;

        var hasLoan = DataStore.loans.Any(x =>
        x.UserId == id && x.ReturnDate == null);

        if (hasLoan == true) 
            return false;

        DataStore.users.Remove(user);

        return true;
    }
}
