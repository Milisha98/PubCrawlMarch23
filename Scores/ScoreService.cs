using PubCrawlMarch23.Drinks;
using PubCrawlMarch23.Helpers;
using PubCrawlMarch23.Rounds;
using PubCrawlMarch23.Users;

namespace PubCrawlMarch23.Scores;

public class ScoreService
{
    private readonly ScoreDA _da;
    private IList<Score> _scores;

    public ScoreService() : this( new ScoreDA() ) { }
    public ScoreService(ScoreDA da)
    {
        _da = da ?? throw new ArgumentNullException(nameof(da));
        _scores = _da.List();
    }
    
    public void AddDrink(RoundID roundID, User user, Drink drink)
    {
        var score = new Score(roundID, user, ScoreEnum.Drink, drink.Points);
        _scores.Add(score);
        _da.Save(_scores);
    }

    public void AddQuiz(RoundID roundID, User user)
    {
        var score = new Score(roundID, user, ScoreEnum.Quiz, 1);
        _scores.Add(score);
        _da.Save(_scores);
    }


    public void AddDare(RoundID roundID, User user)
    {
        var score = new Score(roundID, user, ScoreEnum.Dare, 1);
        _scores.Add(score);
        _da.Save(_scores);
    }

    public int RoundScore(RoundID roundID, User user)
    {
        return _scores
            .Where(s => s.RoundId   == roundID &&
                        s.User.Code == user.Code)
            .Sum(s => s.Points);
    }
    
    public int PlayerOverallDrinkScore(User user) =>
        _scores.Where(s => s.User.Code == user.Code && 
                           s.ScoreType == ScoreEnum.Drink)
               .Sum(s => s.Points);

}
