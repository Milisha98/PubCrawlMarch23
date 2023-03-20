using PubCrawlMarch23.Quiz;

namespace PubCrawlMarch23.Dare;

public class DareViewModel
{
    public readonly DareService _service;

	/// <summary>
	/// Constructor
	/// </summary>
	public DareViewModel() : this(new()) { }
	public DareViewModel(DareService service)
	{
		_service = service ?? new();
	}

    public PageViewEnum PageView { get; set; } = PageViewEnum.List;

    public IList<Dare> Dares { get => _service.Dares; }

    #region Dare Fields

    public Guid DareID { get; set; } = Guid.Empty;
	public string DareName { get; set; } = string.Empty;
    public int Score { get; set; } = 0;
	public string Image { get; set; } = string.Empty;

	public string DareNameDisplay { get => PageView == PageViewEnum.Edit ? DareName : "Dare Name"; }
    public string ImageDisplay { get => PageView == PageViewEnum.Edit ? Image : "Image"; }

    #endregion

    #region Validation

    public bool Validate()
    {
        var errors = new List<string>();

        if (DareName.Length == 0) errors.Add("Please enter a Dare Name");
        if (Image.Length == 0) errors.Add("Pleaes add an Image");
        if (Score < 1) errors.Add("The score must be a positive number");

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

    public void EditItem(Dare dare)
    {
        SetDareFields(dare);
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

        var dare = ToDare();
        _service.AddNew(dare);

        Clear();
        PageView = PageViewEnum.List;

        return true;
    }

    public bool OnUpdate()
    {
        ClearErrors();
        if (Validate() == false) return false;

        var dare = ToDare();
        _service.Update(dare);

        Clear();
        PageView = PageViewEnum.List;

        return true;
    }

    public void OnDelete()
    {
        _service.Delete(DareID);
        Clear();
        PageView = PageViewEnum.List;

    }

    #endregion

    #region Helper Methods

    private void SetDareFields(Dare dare)
    {
        DareID = dare.DareID;
        DareName = dare.DareName;
        Image = dare.Image;
        Score = dare.Score;        
    }

    private void Clear()
    {
        DareID = Guid.Empty;
        DareName = string.Empty;
        Image = string.Empty;
        Score = 0;
    }

    public void ClearErrors()
    {
        IsSuccessful = true;
        Errors = new List<string>();
    }

    private Dare ToDare() =>
        new(DareID,
            DareName,
            Score,
            Image);

    #endregion
}
