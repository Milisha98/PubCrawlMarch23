using PubCrawlMarch23.MessageLogs;
using PubCrawlMarch23.Users;

namespace PubCrawlMarch23.Quiz;

public class QuizService
{
    private readonly QuizQuestionsDA _questionDA;
    private readonly QuizPlayerDA _quizPlayerDA;
    private readonly MessageLogService _logs;

    private List<QuizQuestion> _questions;
    private List<QuizPlayer> _quizPlayers;

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
        _questions = _questionDA.List().ToList();
        _quizPlayers = _quizPlayerDA.List().ToList();
    }


    #region CRUD

    public void AddNew(QuizQuestion question)
    {
        var newQuestion = question with { QuestionID = Guid.NewGuid() };
        _questions.Add(newQuestion);
        _questionDA.Save(_questions);
    }

    public void Update(QuizQuestion question) 
    { 
        _questions.RemoveAll(x => x.QuestionID == question.QuestionID);
        _questions.Add(question);
        _questionDA.Save(_questions);
    }

    public void Delete(QuizQuestion question) 
    { 
        _questions.Remove(question);
        _questionDA.Save(_questions);
    }

    #endregion


    public QuizQuestion? GetQuizQuestionForPlayer(string userCode)
    {
        var alreadyAnswered = _quizPlayers.Where(x => x.UserCode == userCode).Select(x => x.QuestionID).ToList();
        var eligibleQuestions = _questions.Where(x => alreadyAnswered.Contains(x.QuestionID) == false);
        if (eligibleQuestions.Any() == false) return null;
        int rnd = Random.Shared.Next(0, eligibleQuestions.Count() - 1);
        return _questions[rnd];
    }

    public void AnswerQuestion(User player, QuizQuestion question, int answer)
    {
        int points = 0;
        if (question.Answer == answer)
        {
            points = 1;
            _logs.CorrectQuizAnswer(player.Name, player.Pokemon);
        }
        else
        {
            _logs.IncorrectQuizAnswer(player.Name, player.Pokemon);
        }

        var result = new QuizPlayer(question.QuestionID, player.Code, points);
        _quizPlayers.Add(result);
        _quizPlayerDA.Save(_quizPlayers);
        
        // TODO: Check leaderboard and send and appropriate message perhaps?

    }

    public List<QuizQuestion> Questions { get => _questions.ToList(); }

}
