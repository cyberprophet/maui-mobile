using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Platform;

using ShareInvest.Mappers;

using System.Reflection;

namespace ShareInvest.Services;

public class PropertyService : IPropertyService
{
    public void SetValuesOfColumn<T>(T tuple, T param) where T : class
    {
        foreach (var property in tuple.GetType()
                                      .GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            var obj = param.GetType()
                           .GetProperty(property.Name)?
                           .GetValue(param);

            string value = obj?.ToString(),

                   existingValue = property.GetValue(tuple)?
                                           .ToString();

            if (string.IsNullOrEmpty(value) || value.Equals(existingValue))
            {
                continue;
            }
            property.SetValue(tuple, obj);
        }
    }
    public static void SetStatusBarColor()
    {
        if (AppTheme.Dark != Application.Current.PlatformAppTheme)
        {
            StatusBar.SetStyle(StatusBarStyle.DarkContent);
        }
#if ANDROID
        if (Platform.CurrentActivity.Window.StatusBarColor != 0)
        {
            Platform.CurrentActivity.Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
        }
#elif IOS

#endif
    }
}