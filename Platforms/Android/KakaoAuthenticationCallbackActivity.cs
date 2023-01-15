using Android.App;
using Android.Content;
using Android.Content.PM;

namespace ShareInvest.Platforms;

[Activity(NoHistory = true,
          LaunchMode = LaunchMode.SingleTop,
          Name = CALLBACK_NAME,
          Exported = true),

 IntentFilter(new[] { Intent.ActionView },
              Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
              DataHost = CALLBACK_HOST,
              DataScheme = CALLBACK_SCHEME)]
public class KakaoAuthenticationCallbackActivity : WebAuthenticatorCallbackActivity
{
    const string CALLBACK_NAME = "com.kakao.sdk.auth.AuthCodeHandlerActivity";
    const string CALLBACK_HOST = "oauth";
    const string CALLBACK_SCHEME = "";
}