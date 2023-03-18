using PubCrawlMarch23.Pokemon;

namespace PubCrawlMarch23.Users;

public record User(string Code, string Name, PokemonEnum Pokemon, bool IsActive);
