using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
        new(
            1,
            "Street Fightter",
            "Fighting",
            19.99M,
            new DateOnly(1993, 8, 23)
        ),
        new(
            2,
            "Final fantasy",
            "RPG",
            19.99M,
            new DateOnly(1993, 8, 23)
        ),
        new(
            3,
            "War thunder",
            "war",
            19.99M,
            new DateOnly(1993, 8, 23)
        )
    ];

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");


        List<GameDto> GetAllGame()
        {
            return games;
        }
        group.MapGet("/", GetAllGame);   // GET /games


        IResult GetGame(int id)
        {
            var game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        }
        group.MapGet("/{id}", GetGame)   // GET /games/1
            .WithName(GetGameEndpointName);


        IResult AddGame(CreateGameDto newGame)
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        }
        group.MapPost("/", AddGame);   // POST /games


        IResult UpdateGames(int id, UpdateGameDto updatedGame)
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        }
        group.MapPut("/{id}", UpdateGames);   // PUT /games/1


        IResult DeletGame(int id)
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NotFound();
        }
        group.MapDelete("/{id}", DeletGame);   // DELETE/ games/1
    }
}