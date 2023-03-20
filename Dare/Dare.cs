namespace PubCrawlMarch23.Dare;

public record Dare (Guid DareID, string DareName, int Score, string Image);

public record PlayerDare(Guid DareID, string UserCode, int Score);

