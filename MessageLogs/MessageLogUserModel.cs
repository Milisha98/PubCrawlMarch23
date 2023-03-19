namespace PubCrawlMarch23.MessageLogs;

public class MessageLogUserModel
{
	private readonly MessageLogService _service;

	public MessageLogUserModel(MessageLogService messageLogService)
	{
		_service = messageLogService ?? new MessageLogService();

	}

    public IList<MessageLog> Messages { get => _service.Messages; }
}
