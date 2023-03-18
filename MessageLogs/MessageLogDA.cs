using PubCrawlMarch23.Helpers;

namespace PubCrawlMarch23.MessageLogs;

public class MessageLogDA : FileDABase<MessageLog>
{
    private const string File = @"messagelog.json";

    public MessageLogDA() : base(File) { }
}
