namespace PubCrawlMarch23.Locations;

public class LocationService
{
    private readonly LocationDA _da;
	private IList<Location> _locations;

	/// <summary>
	/// Constructor
	/// </summary>
	public LocationService() : this(new LocationDA()) { }
	public LocationService(LocationDA da)
	{
		_da = da ?? new LocationDA();
		_locations = _da.List()
						.OrderBy(x => x.Sequence)
						.ToList();

	}

	public void AddNewLocation(string locationName)
	{
		int sequence = _locations.Any() ? _locations.Max(x => x.Sequence) + 2 : 2;
		locationName = locationName.Trim();


		var location = new Location(sequence, locationName, true);
		_locations.Add(location);
		_da.Save(_locations);

	}

	public void UpdateExistingLocation(int sequence, string locationName, bool isActive) 
	{
		var existing = _locations.FirstOrDefault(x => x.Sequence == sequence);
		if (existing != null)
		{
            _locations.Remove(existing);
        }

		var newLocation = new Location(sequence, locationName, isActive);		
		_locations.Add(newLocation);
		_da.Save(_locations);
	}

	public void RemoveLocation(Location location)
	{
		_locations.Remove(location);
		ReIndex();
	}

    #region Move Methods

    public void MoveUp(Location location)
	{
		if (location.Sequence <= 2) return;
		Move(location, location.Sequence - 3);
	}

	public void MoveDown(Location location) 
	{ 
		int max = _locations.Max(x => x.Sequence);
		if (location.Sequence == max) return;
		Move(location, location.Sequence + 3);
	}

	private void Move(Location location, int newSequence)
	{
        var newLocation = location with { Sequence = newSequence };
        _locations.Remove(location);
		_locations.Add(newLocation);
		ReIndex();
    }

	private void ReIndex()
	{
		_locations = _locations
						.OrderBy(x => x.Sequence)
						.Select((x, i) => x with { Sequence = i * 2 })
						.ToList();
		_da.Save(_locations);
	}

    #endregion

    public IList<Location> Locations { get => _locations; }

}
