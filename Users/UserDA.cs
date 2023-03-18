using System.Runtime.CompilerServices;
using System.Text.Json;
using PubCrawlMarch23.Helpers;

namespace PubCrawlMarch23.Users;

public class UserDA : FileDABase<User>
{
    private const string File = @"users.json";

    public UserDA() : base(File) { }

}
