using PubCrawlMarch23.Rounds;

namespace PubCrawlMarch23.Helpers;

public static class Common
{
    public static string DataPath = @".\Data\";
    public static RoundID? ActiveRound { get; set; } = null;
}
