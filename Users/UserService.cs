using PubCrawlMarch23.Pokemon;
using PubCrawlMarch23.Helpers;
using PubCrawlMarch23.MessageLogs;

namespace PubCrawlMarch23.Users;

public class UserService
{
    private readonly UserDA _da;
    private readonly MessageLogService _logs;
    private IList<User> _users;


    public UserService() : this(new UserDA(), new MessageLogService()) { }
    public UserService(UserDA da, MessageLogService logs)
    {
        _da = da ?? new UserDA();
        _logs = logs ?? new MessageLogService();
        _users = _da.List();
    }

    public User RegisterNewUser(string name, PokemonEnum pokemon)
    {
        // Check if the name already exists
        name = name.Trim();
        User? user = _users.FirstOrDefault(x => string.Equals(name, x.Name, StringComparison.OrdinalIgnoreCase));
        if (user is not null) return user;

        // Generate a Code
        string code = Helper.GenerateCode();
        while (_users.Any(u => u.Code== code)) code = Helper.GenerateCode();

        // Save
        user = new(code, name, pokemon, true);
        _users.Add(user);
        _da.Save(_users);

        // Write an entry in the logs
        _logs.AddNewUserMessage(name, pokemon);

        return user;

    }

    public User RetireUser(User user)
    {
        var newUser = user with { IsActive = false };

        _users.Remove(user);
        _users.Add(newUser);       
        _da.Save(_users);

        // Write an entry in the logs
        _logs.RetireUserMessage(user.Name, user.Pokemon);

        return newUser;

    }

    public IList<User> Users { get => _users; }
}
