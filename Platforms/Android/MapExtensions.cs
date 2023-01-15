using Android.Gms.Maps.Model;

using Microsoft.Maui.Maps.Handlers;

using ShareInvest.Controls;

namespace ShareInvest.Platforms;

public static class MapExtensions
{
    public static void AddAnnotation(this CustomizeMap customize)
    {
        var mapHandler = customize.Map?.Handler as IMapHandler;
        var googleMap = mapHandler?.Map;

        if (mapHandler is not null &&
            googleMap is not null &&
            customize.Map is not null)
        {
            googleMap.SetMapStyle(new MapStyleOptions(customize.MapStyle?
                                                               .JsonStyle
                                                               .ToString()));
        }
    }
    public static void AddAnnotation(this CustomizePin customize)
    {
        var mapHandler = (IMapHandler)customize.Map?.Handler;
        var googleMap = mapHandler?.Map;

        if (mapHandler is not null &&
            googleMap is not null &&
            customize.Map is not null)
        {
            var stream = new MemoryStream(Properties.Resources.SILVER_MAP_STYLE);

            string json;

            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
            googleMap.SetMapStyle(new MapStyleOptions(json));
        }
    }
}