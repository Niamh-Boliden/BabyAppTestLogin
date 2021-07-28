using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Identity.Client;

namespace BabyAppTestLogin.Droid
{
    //IDK WHAT IM DOING
    [Activity]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataHost = "auth",
        DataScheme = "msal0ed9265f-f44c-40da-9a29-7c6c58602ec6")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}