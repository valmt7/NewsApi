
using System.Globalization;
using System.Net;
using NewsApi.Objects;
namespace NewsApi.Service.RAWG;
using System;
using RestSharp;
using Newtonsoft.Json;

public class Game : IGame
{
    private readonly string _RAWG_API_KEY;
    private readonly IPGSQL _pgsql;

    public Game(IConfiguration config, IPGSQL ipgsql)
    {
        _RAWG_API_KEY = config["RAWG_API_KEY"];
        _pgsql = ipgsql;
    }

    public async Task<FindedGameobjects> FindGame(string gameName)
    {
        var optionRAWG = new RestClientOptions("https://api.rawg.io/api");
        using var clientRAWG = new RestClient(optionRAWG);
        var requestRAWG = new RestRequest("/games", Method.Get);
        requestRAWG.AddQueryParameter("key", _RAWG_API_KEY);
        requestRAWG.AddQueryParameter("search", gameName);

        var responseRAWG = await clientRAWG.ExecuteAsync(requestRAWG);

        if (responseRAWG.StatusCode == HttpStatusCode.OK)
        {
            var responseResults = JsonConvert.DeserializeObject<RAWGFindedGameObjects>(responseRAWG.Content);
            var result = responseResults?.Results?.FirstOrDefault();


            if (result == null)
            {
                return null;
            }

            var findedGame = new FindedGameobjects()
            {
                Id = result.Id,
                Name = result.Name,
                Released = result.Released,
                BackgroundImage = result.BackgroundImage,
                Metacritic = result.Rating,
                Platforms = result.Platforms,
            };

            var optionCHEAP = new RestClientOptions("https://www.cheapshark.com/api/1.0");
            using var cheapClient = new RestClient(optionCHEAP);
            var requestCHEAP = new RestRequest("/deals", Method.Get);
            requestCHEAP.AddQueryParameter("title", gameName);

            var responseCHEAP = await cheapClient.ExecuteAsync(requestCHEAP);

            if (responseCHEAP.IsSuccessful)
            {
                var cheapList = JsonConvert.DeserializeObject<List<ChepSharkObject>>(responseCHEAP.Content);


                var resultCheap = cheapList?.FirstOrDefault();


                if (resultCheap != null)
                {
                    findedGame.IsDealsActive = true;
                    findedGame.Saleprice = resultCheap.salePrice;
                    findedGame.NormalPrice = resultCheap.normalPrice;
                    findedGame.Thumb = resultCheap.thumb;
                }
                else
                {

                    SetNoDeals(findedGame);
                }
            }
            else
            {

                SetNoDeals(findedGame);
            }

            return findedGame;
        }

        throw new Exception("RAWGAPI Error: " + responseRAWG.StatusCode);
    }

    public async Task<List<FreeGameApiObject>> GetLastFiveFreeGames(string userId)
    {
        var option = new RestClientOptions("https://www.freetogame.com/api");
        using var client = new RestClient(option);
        var request = new RestRequest("/games", Method.Get);
        request.AddQueryParameter("sort-by", "release-date");
        var response = await client.ExecuteAsync(request);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = new List<FreeGameApiObject>();
            var responseResults = JsonConvert.DeserializeObject<List<FreeGameApiObject>>(response.Content);
            for (int i = 0; i < 5; i++)
            {
                result.Add(responseResults[i]);
            }

            return result;


        }
        return null;
    }
    public async Task<List<FreeGameApiObject>> GetLastFreeGames(string userId)
    {
        var option = new RestClientOptions("https://www.freetogame.com/api");
        using var client = new RestClient(option);
        var request = new RestRequest("/games", Method.Get);
        request.AddQueryParameter("sort-by", "release-date");
        var response = await client.ExecuteAsync(request);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            int userLastGameId = await _pgsql.GetLastGameId(userId);
            var result = new List<FreeGameApiObject>();
            var responseResults = JsonConvert.DeserializeObject<List<FreeGameApiObject>>(response.Content);
            var lastGameId = responseResults.First().id;
            if (userLastGameId == -1)
            {
                for (int i = 0; i < 5; i++)
                {
                    result.Add(responseResults[i]);
                }
            }
            else
            {
                
                for (int i = 0; i < responseResults.Count(); i++)
                {
                    if (userLastGameId == responseResults[i].id)
                    {
                        break;
                    }
                    result.Add(responseResults[i]);
                }
            }
            await _pgsql.EditLastGameId(userId, lastGameId);
            return result;


        }
        return null;
    }
    


private void SetNoDeals(FindedGameobjects gameobjects)
{
    gameobjects.IsDealsActive = false;
    gameobjects.Saleprice = "Not found";
    gameobjects.NormalPrice = "Not found";
    gameobjects.Thumb = "Not found";
}
}