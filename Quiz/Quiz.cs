namespace PubCrawlMarch23.Quiz;

public record QuizQuestion(Guid QuestionID, string Question, string Answer1, string Answer2, string Answer3, string Answer4, int Answer);

public record QuizPlayer(Guid QuestionID, string PlayerCode, int Points);