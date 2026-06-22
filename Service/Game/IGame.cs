namespace NewsApi.Service.RAWG;
using NewsApi.Objects;
public interface IGame
{
    public Task<FindedGameobjects> FindGame(string gameName);
    public Task<List<FreeGameApiObject>> GetLastFiveFreeGames(string userId);
}