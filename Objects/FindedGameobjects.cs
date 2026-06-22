namespace NewsApi.Objects;

public class FindedGameobjects
{
    public int Id{ get; set; }
    public string Name { get; set; }
    public string Released {get;set; }
    public string BackgroundImage { get; set; }
    public double Metacritic { get; set; }
    public List<PlatformWrapper> Platforms { get; set; }
    public bool IsDealsActive { get; set; }
    public string Saleprice { get; set; }
    public string NormalPrice { get; set; }
    public string Thumb { get; set; }
    

}