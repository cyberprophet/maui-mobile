using ShareInvest.Models;
using ShareInvest.Platforms;

namespace ShareInvest.Controls;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;

public class CustomizeMap : Map
{
    public Microsoft.Maui.Maps.IMap Map
    {
        get; set;
    }
    public MapStyle MapStyle
    {
        get => (MapStyle)GetValue(MapStyleProperty);

        set => SetValue(MapStyleProperty, value);
    }
    public static readonly BindableProperty MapStyleProperty =

        BindableProperty.Create(propertyName: nameof(MapStyle),
                                returnType: typeof(MapStyle),
                                declaringType: typeof(CustomizeMap),
                                defaultValue: null,
                                defaultBindingMode: BindingMode.OneWay,
                                propertyChanged: (bindable, oldValue, newValue) =>
                                {
                                    if (bindable is CustomizeMap control &&
                                        newValue is MapStyle)
                                    {
#if ANDROID
                                        control.AddAnnotation();
#elif IOS || MACCATALYST

#endif
                                    }
                                    else
                                    {
#if ANDROID

#elif IOS

#endif
                                    }
                                });
}