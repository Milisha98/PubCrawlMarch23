using PubCrawlMarch23.Locations;
using PubCrawlMarch23.Users;

namespace PubCrawlMarch23.Rounds;

public record RoundID (Guid ID);
public record Round (RoundID RoundId, Location Location, bool IsActive);
public record Verses (RoundID RoundId, User Player1, User Player2);
