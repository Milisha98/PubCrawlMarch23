using PubCrawlMarch23.Helpers;
using PubCrawlMarch23.Locations;

namespace PubCrawlMarch23.Scores;

public class ScoreDA : FileDABase<Score>
{
    private const string File = @"scores.json";

    public ScoreDA() : base(File) { }

}
