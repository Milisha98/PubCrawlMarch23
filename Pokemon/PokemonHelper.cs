namespace PubCrawlMarch23.Pokemon;

public static class PokemonHelper
{
    public static string GetImage(this PokemonEnum pokemon) =>
        $"images/{pokemon}.jpg";

    public static List<PokemonEnum> List() =>
        new()
        {
            PokemonEnum.Absol,
            PokemonEnum.Ampharos,
            PokemonEnum.Arcanine,
            PokemonEnum.Blaziken,
            PokemonEnum.Bulbasaur,
            PokemonEnum.Charizard,
            PokemonEnum.Dragonite,
            PokemonEnum.Eevee,
            PokemonEnum.Flygon,
            PokemonEnum.Gardevoir,
            PokemonEnum.Gengar,
            PokemonEnum.Lucario,
            PokemonEnum.Ninetales,
            PokemonEnum.Pikachu,
            PokemonEnum.Squirtle,
            PokemonEnum.Torterra,
            PokemonEnum.Typhlosion,
            PokemonEnum.Umbreon
        };
}
