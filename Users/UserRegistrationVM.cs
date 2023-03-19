using PubCrawlMarch23.Helpers;
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

    public bool Validate()
    {
        var errors = new List<string>();

        if (UserName.Length == 0) errors.Add("Please enter your name");
        if (SelectedPokemon is null) errors.Add("Please select a Pokemon");
     
        if (_userService.Users.Any(u => string.Equals(UserName, u.Name, StringComparison.InvariantCultureIgnoreCase)))
        {
            errors.Add("You have already registered. Please talk to a dev and we can retreive your code if you've lost it");
        }

        IsSuccessful = !errors.Any();
        Errors = errors;

        return IsSuccessful;
    }

    public bool Register()
    {
        if (Validate() == false) return false;
        var user = _userService.RegisterNewUser(UserName, SelectedPokemon.Value);
        if (user == null) return false;
        UserCode= user.Code;
        return true;
    }

    private string _userName = string.Empty;
    public string UserName
    {
        set
        {
            value = Regex.Replace(value, @"[^\w ']", "");
            if (value.Length > 50) value = value[..50];

            _userName = value.Trim();

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

    public string UserCode { get; private set; } = string.Empty;
    public bool IsSuccessful { get; private set; }
    public List<string> Errors { get; private set; } = new List<string>();

}
