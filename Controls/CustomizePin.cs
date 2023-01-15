using Microsoft.Maui.Controls.Maps;

using ShareInvest.Platforms;

namespace ShareInvest.Controls;

public class CustomizePin : Pin
{
    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);

        set => SetValue(ImageSourceProperty, value);
    }
    public Microsoft.Maui.Maps.IMap Map
    {
        get; set;
    }
    public static readonly BindableProperty ImageSourceProperty =

        BindableProperty.Create(nameof(ImageSource),
                                typeof(ImageSource),
                                typeof(CustomizePin),
                                propertyChanged: OnImageSourceChanged);

    static void OnImageSourceChanged(BindableObject bindable,
                                     object oldValue,
                                     object newValue)
    {
        var control = (CustomizePin)bindable;
        var imageSource = control.ImageSource;

        if (control.Handler?.PlatformView is null)
        {
            control.HandlerChanged += OnHandlerChanged;

            return;
        }
        if (imageSource is not null)
        {
#if ANDROID
            control.AddAnnotation();
#elif  IOS || MACCATALYST
			
#endif
        }
        else
        {
#if ANDROID

#elif IOS

#endif
        }
        void OnHandlerChanged(object sender, EventArgs e)
        {
            OnImageSourceChanged(control, oldValue, newValue);

            control.HandlerChanged -= OnHandlerChanged;
        }
    }
}