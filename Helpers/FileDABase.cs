using PubCrawlMarch23.Helpers;

namespace PubCrawlMarch23;

public abstract class FileDABase<T>
{
    public FileDABase(string fileName)
	{
        FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
	}

    public IList<T> List() =>
        FilePath.ReadFile()
                .Deserialize<IList<T>>() ?? new List<T>();

    public void Save(IList<T> items) =>
        items.Serialize()
             .SaveFile(FilePath);

    public string FileName { get; init; }
    public string FilePath { get => Path.Combine(Common.DataPath, FileName); }

}
