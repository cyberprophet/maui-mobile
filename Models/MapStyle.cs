namespace ShareInvest.Models;

public sealed class MapStyle
{
    public static MapStyle FromJson(string jsonStyle)
    {
        return new MapStyle(jsonStyle);
    }
    public string JsonStyle
    {
        get;
    }
    MapStyle(string jsonStyle)
    {
        JsonStyle = jsonStyle;
    }
}