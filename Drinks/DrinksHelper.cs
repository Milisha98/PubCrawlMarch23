namespace PubCrawlMarch23.Drinks;

public static class DrinksHelper
{
    public static string DrinkDescription(this int points) =>
        points switch
        {
            0 => "Stone Cold Sober",
            <= 2 => "Cadbury Kid",
            <= 4 => "Warming Up",
            <= 6 => "Seasoned Trooper",
            <= 8 => "Thoroughly Inebriated",            
            <= 10 => "Capricorn Employee",
            <= 12 => "Raging Alcoholic",
            > 12 => "Mel Gibson"
        };
}
