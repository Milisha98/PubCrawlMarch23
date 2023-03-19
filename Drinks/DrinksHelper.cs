namespace PubCrawlMarch23.Drinks;

public static class DrinksHelper
{
    public static string DrinkName(this DrinkEnum code) =>
        code switch
        {
            DrinkEnum.BeerMidi => "Beer (Midi)",
            DrinkEnum.BeerPint => "Beer (Pint)",
            DrinkEnum.Wine => "Wine",
            DrinkEnum.Sprit => "Spirit",
            DrinkEnum.Cocktail => "Cocktail",
            _ => "Alcohol"
        };

    public static int DrinkPoints(this DrinkEnum code) =>
    code switch
    {
        DrinkEnum.BeerMidi => 1,
        DrinkEnum.BeerPint => 2,
        DrinkEnum.Wine => 2,
        DrinkEnum.Sprit => 2,
        DrinkEnum.Cocktail => 3,
        _ => 1
    };

    public static Drink ToDrink(this DrinkEnum code) =>
        new(code, code.DrinkName(), code.DrinkPoints());

    public static string DrunkDescription(this int points) =>
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
