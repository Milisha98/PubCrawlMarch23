namespace PubCrawlMarch23.Helpers;

public static class Helper
{
    public static string GenerateCode()
    {
        const int length = 4;
        string from = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var sb = new System.Text.StringBuilder();
        for (int i = 0; i < length; i++)
        {
            int rnd = System.Random.Shared.Next(0, from.Length);
            sb.Append(from[rnd]);
        }
        return sb.ToString();

    }

}
