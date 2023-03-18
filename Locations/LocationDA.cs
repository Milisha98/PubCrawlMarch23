using PubCrawlMarch23.Helpers;

namespace PubCrawlMarch23.Locations;

public class LocationDA : FileDABase<Location>
{
    private const string File = @"locations.json";
    public LocationDA() : base(File) { }

}
