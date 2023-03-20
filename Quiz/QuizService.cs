using PubCrawlMarch23.MessageLogs;
using PubCrawlMarch23.Users;

namespace PubCrawlMarch23.Quiz;

public class QuizService
{
    private readonly QuizQuestionsDA _questionDA;
    private readonly QuizPlayerDA _quizPlayerDA;
    private readonly MessageLogService _logs;

    private IList<QuizQuestion> _questions;
    private IList<QuizPlayer> _quizPlayers;

    /// <summary>
    /// Constructor
    /// </summary>
    public QuizService() : this(new(), new(), new()) { }
    public QuizService(QuizQuestionsDA questionDA, 
                       QuizPlayerDA quizPlayerDA, 
                       MessageLogService logs)
    {
        _questionDA = questionDA ?? new();
        _quizPlayerDA = quizPlayerDA ?? new();
        _logs = logs ?? new();
        _questions = _questionDA.List();
        _quizPlayers = _quizPlayerDA.List();
    }

    public QuizQuestion? GetQuizQuestionForPlayer(string playerCode)
    {
        var alreadyAnswered = _quizPlayers.Where(x => x.PlayerCode == playerCode).Select(x => x.QuestionID).ToList();
        var eligibleQuestions = _questions.Where(x => alreadyAnswered.Contains(x.QuestionID) == false);
        if (eligibleQuestions.Any() == false) return null;
        int rnd = Random.Shared.Next(0, eligibleQuestions.Count() - 1);
        return _questions[rnd];
    }

    public void AnswerQuestion(User player, Guid questionID, int answer)
    {
        int points = 0;
        var question = _questions.FirstOrDefault(x => x.QuestionID == questionID);
        if (question == null) return;
        if (question.Answer == answer)
        {
            points = 1;
            _logs.CorrectQuizAnswer(player.Name, player.Pokemon);
        }
        else
        {
            _logs.IncorrectQuizAnswer(player.Name, player.Pokemon);
        }

        var result = new QuizPlayer(questionID, player.Code, points);
        _quizPlayers.Add(result);
        _quizPlayerDA.Save(_quizPlayers);
        
        // TODO: Check leaderboard and send and appropriate message perhaps?

    }

    public IList<QuizQuestion> Questions { get => _questions; }

}
