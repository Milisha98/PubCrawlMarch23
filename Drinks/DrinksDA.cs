namespace PubCrawlMarch23.Drinks;

public class DrinksDA
{
    public List<Drink> List() =>
        new()
        {
            new("Beer (Midi)", 1),
            new("Beer (Pint)", 2),
            new("Wine", 2),
            new("Sprits", 2),
            new("Cocktail", 3)
        };

}
