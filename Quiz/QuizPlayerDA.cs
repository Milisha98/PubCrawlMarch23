namespace PubCrawlMarch23.Quiz;

public class QuizPlayerDA : FileDABase<QuizPlayer>
{
    private const string File = @"quiz-player.json";
    public QuizPlayerDA() : base(File) { }

}
