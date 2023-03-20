namespace PubCrawlMarch23.Dare;

public class PlayerDareDA : FileDABase<PlayerDare>
{
    const string File = @"player-dare.json";

    public PlayerDareDA() : base(File) { }
}
