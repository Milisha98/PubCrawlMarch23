namespace PubCrawlMarch23.Dare;

public class DareDA : FileDABase<Dare>
{
    const string File = @"dares.json";
	public DareDA() : base(File) { }

}
