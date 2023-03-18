using System.Text.Json;

namespace PubCrawlMarch23.Helpers;

/// <summary>
/// Only exists to ensure concurrency by a simple block & wait strategy
/// </summary>
public static class FileHelper
{
    static readonly object _object = new();

    public static void SaveFile(this string content, string filePath)
    {
        lock (_object)
        {
            File.WriteAllText(filePath, content);
        }
    }

    public static string ReadFile(this string filePath) 
    {
        lock (_object)
        {
            if (File.Exists(filePath) == false) return string.Empty;
            return File.ReadAllText(filePath);
        }
    }

    public static T? Deserialize<T>(this string jsonString) =>
        jsonString.Length > 1 ? JsonSerializer.Deserialize<T>(jsonString) : default;
    
    public static string Serialize<T>(this T obj) =>
        JsonSerializer.Serialize(obj);

}
