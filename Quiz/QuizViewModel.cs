using PubCrawlMarch23.Locations;

namespace PubCrawlMarch23.Quiz;

public class QuizViewModel
{
    private readonly QuizService _service;

    /// <summary>
    /// Constructor
    /// </summary>
    public QuizViewModel() : this(new()) { }
    public QuizViewModel(QuizService service)
    {
        _service = service ?? new();
    }

    public PageViewEnum PageView { get; set; } = PageViewEnum.List;

    public IList<QuizQuestion> Questions { get => _service.Questions; }

    #region Quiz Question Fields

    public Guid QuestionID { get; set; } = Guid.Empty;
    public string Question { get; set; } = string.Empty;
    public string Answer1 { get; set; } = string.Empty;
    public string Answer2 { get; set; } = string.Empty;
    public string Answer3 { get; set; } = string.Empty;
    public string Answer4 { get; set; } = string.Empty;
    public int Answer { get; set; }

    public string QuestionDisplay { get => PageView == PageViewEnum.Edit ? Question : "Question"; }
    public string Answer1Display { get => PageView == PageViewEnum.Edit ? Answer1 : "Answer 1"; }
    public string Answer2Display { get => PageView == PageViewEnum.Edit ? Answer2 : "Answer 2"; }
    public string Answer3Display { get => PageView == PageViewEnum.Edit ? Answer3 : "Answer 3"; }
    public string Answer4Display { get => PageView == PageViewEnum.Edit ? Answer4 : "Answer 4"; }

    #endregion

    #region Validation

    public bool Validate()
    {
        var errors = new List<string>();

        if (Question.Length == 0) errors.Add("Please enter a Quiz Question");
        if (Answer1.Length == 0) errors.Add("Pleaes add an Answer 1");
        if (Answer2.Length == 0) errors.Add("Pleaes add an Answer 2");
        if (Answer3.Length == 0) errors.Add("Pleaes add an Answer 3");
        if (Answer4.Length == 0) errors.Add("Pleaes add an Answer 4");
        if (Answer < 1 || Answer > 4) errors.Add("The answer should be between 1 and 4");

        IsSuccessful = !errors.Any();
        Errors = errors;

        return IsSuccessful;
    }
    public bool IsSuccessful { get; private set; }
    public List<string> Errors { get; private set; } = new List<string>();

    #endregion

    #region Form Actions

    public void AddNewItem()
    {
        Clear();
        PageView = PageViewEnum.AddNew;
    }

    public void EditItem(QuizQuestion question)
    {
        SetQuizFields(question);
        PageView = PageViewEnum.Edit;
    }

    public void Cancel()
    {
        ClearErrors();
        Clear();
        PageView = PageViewEnum.List;
    }

    #endregion

    #region CRUD Actions

    public void OnSave()
    {
        if (PageView == PageViewEnum.Edit)
            OnUpdate();
        else if (PageView == PageViewEnum.AddNew)
            OnInsert();
    }

    public bool OnInsert()
    {
        ClearErrors();
        if (Validate() == false) return false;

        var question = ToQuizQuestion();
        _service.AddNew(question);

        Clear();
        PageView = PageViewEnum.List;

        return true;
    }

    public bool OnUpdate()
    {
        ClearErrors();
        if (Validate() == false) return false;

        var question = ToQuizQuestion();
        _service.Update(question);

        Clear();
        PageView = PageViewEnum.List;

        return true;
    }

    public void OnDelete()
    {
        var question = Questions.FirstOrDefault(x => x.QuestionID == QuestionID);
        if (question is not null) 
        { 
            _service.Delete(question);
        }

        Clear();
        PageView = PageViewEnum.List;

    }

    #endregion

    #region Helper Methods

    private void SetQuizFields(QuizQuestion quiz)
    {
        QuestionID = quiz.QuestionID;
        Question = quiz.Question;
        Answer1 = quiz.Answer1;
        Answer2 = quiz.Answer2;
        Answer3 = quiz.Answer3;
        Answer4 = quiz.Answer4;
        Answer = quiz.Answer;
    }

    private void Clear()
    {
        QuestionID = Guid.Empty;
        Question = string.Empty;
        Answer1 = string.Empty;
        Answer2 = string.Empty;
        Answer3 = string.Empty;
        Answer4 = string.Empty;
        Answer = 0;
    }

    public void ClearErrors()
    {
        IsSuccessful = true;
        Errors = new List<string>();
    }

    private QuizQuestion ToQuizQuestion() =>
        new (QuestionID, 
             Question, 
             Answer1,
             Answer2, 
             Answer3, 
             Answer3, 
             Answer);       
    

    #endregion
}
