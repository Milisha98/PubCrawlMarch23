using PubCrawlMarch23.Quiz;
using PubCrawlMarch23.Users;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PubCrawlMarch23.Locations;

public class LocationViewModel
{
    private readonly LocationService _service;

	/// <summary>
	/// Constructor
	/// </summary>
	public LocationViewModel() : this (new LocationService()) { }
	public LocationViewModel(LocationService locationService)
	{
		_service = locationService ?? new LocationService();
	}

    public PageViewEnum PageView { get; set; } = PageViewEnum.List;

    public IList<Location> Locations { get => _service.Locations; }

    #region Location Fields

    private string _locationName = string.Empty;
	public string LocationName
	{
		get { return _locationName; }
		set 
		{
            value = Regex.Replace(value, @"[^\w ']", "");
            if (value.Length > 50) value = value[..50];
            _locationName = value;		
		}
	}

    public bool IsActive { get; set; }
    public int Sequence { get; set; }

    public string LocationNameDisplay { get => PageView == PageViewEnum.Edit ? LocationName : "Location"; }

    #endregion

    #region Validation

    public bool Validate(bool validateDuplicate)
    {
        var errors = new List<string>();

        if (LocationName.Length == 0) errors.Add("Please enter a Location Name");

        // On Add New also ensure that the location name is new
        if (validateDuplicate)
        {
            if (_service.Locations.Any(u => string.Equals(LocationName, u.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                errors.Add("There is already a location of this name");
            }
        }

        IsSuccessful = !errors.Any();
        Errors = errors;

        return IsSuccessful;
    }

    public bool IsSuccessful { get; private set; }
    public List<string> Errors { get; private set; } = new List<string>();

    #endregion

    #region Form Actions

    public void OnSave()
    {
        if (PageView == PageViewEnum.Edit)
            OnUpdate();
        else if (PageView == PageViewEnum.AddNew)
            OnInsert();
    }

    public void AddNewItem()
    {
        Clear();
        PageView = PageViewEnum.AddNew;
    }

    public void EditItem(Location location)
    {
        SetLocationFields(location);
        PageView = PageViewEnum.Edit;
    }

    public void Cancel()
    {
        ClearErrors();
        Clear();
        PageView = PageViewEnum.List;
    }

    public bool CanMoveUp(Location location) =>
        location.Sequence > 2;
    public bool CanMoveDown(Location location) =>
        Locations.Any() && location.Sequence < Locations.Max(x => x.Sequence);

    public void MoveUp(Location location)
    {
        if (CanMoveUp(location)) _service.MoveUp(location);
    }

    public void MoveDown(Location location)
    {
        if (CanMoveDown(location)) _service.MoveDown(location);
    }

    #endregion

    #region CRUD Actions

    public bool OnInsert()
    {
        ClearErrors();
        if (Validate(true) == false) return false;

        var location = ToLocation();
        _service.AddNewLocation(location);

        Clear();
        PageView = PageViewEnum.List;

        return true;

    }

    public bool OnUpdate()
    {
        ClearErrors();
        if (Validate(false) == false) return false;

        var location = ToLocation();
        _service.UpdateExistingLocation(location);

        Clear();
        PageView = PageViewEnum.List;

        return true;
    }

    public void OnDelete()
    {
        _service.RemoveLocation(Sequence);

        Clear();
        PageView = PageViewEnum.List;

    }

    #endregion

    #region Helper Methods

    private void SetLocationFields(Location location)
    {
        LocationName = location.Name;
        Sequence = location.Sequence;
        IsActive = location.IsActive;
    }

    private void Clear()
    {
        LocationName = string.Empty;
        Sequence = 0;
        IsActive = false;
    }

    public void ClearErrors()
    {
        IsSuccessful = true;
        Errors = new List<string>();
    }

    private Location ToLocation() =>
        new(Sequence, LocationName, IsActive);


    #endregion
}
