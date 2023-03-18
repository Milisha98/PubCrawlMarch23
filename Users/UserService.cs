using PubCrawlMarch23.Pokemon;
using PubCrawlMarch23.Helpers;

namespace PubCrawlMarch23.Users;

public class UserService
{
    private UserDA _da;
    private IList<User> _users;

    public UserService() : this(new UserDA()) { }
    public UserService(UserDA da)
    {
        _da = da ?? throw new ArgumentNullException(nameof(da));
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
        return user;

    }

    public IList<User> Users { get => _users; }
}
