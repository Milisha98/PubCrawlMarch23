namespace PubCrawlMarch23.Locations;

public class LocationService
{
    private readonly LocationDA _da;
	private List<Location> _locations;

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

	public void AddNewLocation(Location location)
	{
		
		int sequence = _locations.Any() ? _locations.Max(x => x.Sequence) + 2 : 2;
		var newLocation = new Location(sequence, location.Name.Trim(), location.IsActive);

		_locations.Add(newLocation);
		_da.Save(_locations);

	}

	public void UpdateExistingLocation(Location location) 
	{
		_locations.RemoveAll(x => x.Sequence == location.Sequence);
		_locations.Add(location);
        ReIndex();
    }

	public void RemoveLocation(int sequence)
	{
        _locations.RemoveAll(x => x.Sequence == sequence);
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
