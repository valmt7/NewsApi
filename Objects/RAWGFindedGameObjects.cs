using System;
using System.Collections.Generic;

namespace NewsApi.Objects
{
    public class RAWGFindedGameObjects
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<GameResult> Results { get; set; }
    }

    public class GameResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Released { get; set; }
        public bool Tba { get; set; }
        public string BackgroundImage { get; set; }
        public double Rating { get; set; }
        public double RatingTop { get; set; }
        public int? Metacritic { get; set; }
        public int Playtime { get; set; }
        public DateTime Updated { get; set; }
        public EsrbRating EsrbRating { get; set; }
        public List<PlatformWrapper> Platforms { get; set; }
    }

    public class EsrbRating
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PlatformWrapper
    {
        public PlatformDetails Platform { get; set; }
        public string ReleasedAt { get; set; }
    }

    public class PlatformDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}