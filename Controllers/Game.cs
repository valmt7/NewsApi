using Microsoft.AspNetCore.Mvc;
using NewsApi.Service.RAWG;
namespace NewsApi.Controllers;
[ApiController]
[Route("api/game")]
public class Game : ControllerBase
{
    private readonly IGame _game;

    public Game(IGame game)
    {
        _game = game;
    }
    [HttpGet("find")]
    public async Task<IActionResult> FindGamne(string gameName)
    {
        var game = await _game.FindGame(gameName); 
    
        if (game == null)
        {
            return NotFound("Гру не знайдено.");
        }

        return Ok(game);
    }

    [HttpGet("fivegames")]
    public async Task<IActionResult> FiveGames(string userId)
    {
        var games = await _game.GetLastFiveFreeGames(userId);
        if (games == null)
        {
            return NotFound("Ігри не знайдено.");
        }
        return Ok(games);
    }

    [HttpGet("lastgamesforuser")]
    public async Task<IActionResult> FindFreeGamesForuser(string userId)
    {
        var games = await _game.GetLastFreeGames(userId);
        if (games == null)
        {
            return NotFound("Ігри не знайдено.");
        }
        return Ok(games);
    }
    
}