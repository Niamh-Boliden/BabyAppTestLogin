﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Plugin.CurrentActivity;
using BabyAppTestLogin.Services;
using Microsoft.Identity.Client;
using Android.Content;
using Xamarin.Forms;

namespace BabyAppTestLogin.Droid
{
    [Activity(Label = "BabyAppTestLogin", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
           // DependencyService.Register<IParentWindowLocatorService, AndroidParentWindowLocatorService>();
            LoadApplication(new App());

            Views.BAconstants.ParentWindow = this;

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }

    }
}