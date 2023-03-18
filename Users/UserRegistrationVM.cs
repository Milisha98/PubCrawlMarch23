using PubCrawlMarch23.Pokemon;
using System.Text.RegularExpressions;

namespace PubCrawlMarch23.Users;

public class UserRegistrationVM
{   
    private readonly UserService _userService;
 
    public UserRegistrationVM(UserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    private string _userName = string.Empty;
    public string UserName
    {
        set
        {
            // Probably want to use the HTML input box pattern property
            value = Regex.Replace(value, @"[^\w ']", "");
            if (value.Length > 50) value = value.Substring(0, 50);

            _userName = value;

        }
        get { return _userName; }
    }

    public PokemonEnum? SelectedPokemon { get; set; } = null;

    public IEnumerable<PokemonEnum> PokemonList
    {
        get =>
            PokemonHelper.List()
                         .Except(_userService.Users.Select(u => u.Pokemon));
    }
}
