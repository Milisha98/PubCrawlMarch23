using PubCrawlMarch23.Pokemon;
using System.Text.RegularExpressions;

namespace PubCrawlMarch23.MessageLogs;

public class MessageLogService
{
    private readonly MessageLogDA _da;
	private List<MessageLog> _messages;

	public MessageLogService() : this(new MessageLogDA()) { }
	public MessageLogService(MessageLogDA messageLogDA)
	{
		_da = messageLogDA ?? new MessageLogDA();
		_messages = new List<MessageLog>();
	}

	public void AddNewUserMessage(string userName, PokemonEnum pokemon)
	{
		var funNight = new List<string>
		{
		  "to party",
          "to show you how it's done",
          "to get sloshed",
          "to get wasted",
          "to get smashed",
          "to be more than a little tipsy",
          "to drink you under the table",
          "to get rowdy",
          "to dance on tables",
		  "and is feeling thirsty",
		  "and is eyeing off your drink",
		  "and is a seasoned practitioner",
		  "and is feeling parched",
		  "and is gasping for a drink"
		};
		var index = Random.Shared.Next(0, funNight.Count);
		string fun = funNight[index];

		string message = $"{userName} ({pokemon}) has signed up {fun}";
		AddMessage(message);

	}

    public void RetireUserMessage(string userName, PokemonEnum pokemon)
	{
		var sucksNight = new List<string>
		{
			"is admiting defeat",
			"might be paying for cleaning in their uber ride",
			"can't keep up",
			"might not be allowed into further establishments",
			"might get a lifetime ban at the pub",
			"could be cautioned with a move-on notice",
			"could be too paralytic",
			"has used up their fun quota",
			"is feeling a hangover coming on",
			"might be blowing chunks",
			"realizes the mistake of their life choices",
			"has to adult up in the morning",
			"is being dishonourably discharged"
		};
		var index = Random.Shared.Next(0, sucksNight.Count);
        string sucks = sucksNight[index];
        string message = $"{userName} ({pokemon}) {sucks} and is retiring";

        AddMessage(message);
    }

	public void DrinkMessage(string userName, PokemonEnum pokemon, string drinkName)
	{
		var drinkSynonym = new List<string>
		{
            "attacked",
			"skulled",
			"chugged",
			"downed",
			"drained",
			"drank",
			"finished off",
			"necked",
			"forced down",
			"glugged",
			"guzzled",
			"imbibed",
			"knocked back",
			"obliterated",
			"polished off",
			"quaffed",
			"sank",
			"skulled",
			"smashed back",
			"wolfed down"
        };
        var index = Random.Shared.Next(0, drinkSynonym.Count);
        string synonym = drinkSynonym[index];
        string message = $"{userName} ({pokemon}) {synonym} a {drinkName}";
		
		AddMessage(message);

    }

    public void AddMessage(string message)
	{
		message = Regex.Escape(message);	// Not tested, but hoping this will prevent messagages from corrupting the json file store
		var log = new MessageLog(DateTime.Now, message);
		_messages.Add(log);
		_da.Save(_messages);
	}

	public List<MessageLog> Messages { get => _messages; }

}
