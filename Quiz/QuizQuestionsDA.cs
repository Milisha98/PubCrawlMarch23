using PubCrawlMarch23;

namespace PubCrawlMarch23.Quiz;

public class QuizQuestionsDA : FileDABase<QuizQuestion>
{
    private const string File = @"quiz-questions.json";
    public QuizQuestionsDA() : base(File) { }

}
