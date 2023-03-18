using PubCrawlMarch23.Rounds;
using PubCrawlMarch23.Users;

namespace PubCrawlMarch23.Scores;

public record Score(RoundID RoundId, User User, ScoreEnum ScoreType, int Points);

public enum ScoreEnum
{
    Drink,
    Quiz,
    Dare
}