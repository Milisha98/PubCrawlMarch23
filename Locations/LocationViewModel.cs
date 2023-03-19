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

    public bool AddNew()
    {
        if (Validate(true) == false) return false;

        _service.AddNewLocation(LocationName);
        return true;
    }

    public bool UpdateExisting()
    {
        if (Validate(false) == false) return false;
        _service.UpdateExistingLocation(Sequence, LocationName, IsActive);
        return true;
    }

    public void Delete() 
    {
        var location = Locations.FirstOrDefault(x => x.Sequence == Sequence);
        if (location == null) return;
        _service.RemoveLocation(location);
        Clear();
        ClearErrors();
    }

    public void Clear()
    {
        Sequence= 0;
        LocationName = string.Empty;
        IsActive = false;
    }

    public void ClearErrors()
    {
        IsSuccessful = true;
        Errors = new List<string>();
    }

    public bool IsSuccessful { get; private set; }
    public List<string> Errors { get; private set; } = new List<string>();

    public IList<Location> Locations { get => _service.Locations; }
}
