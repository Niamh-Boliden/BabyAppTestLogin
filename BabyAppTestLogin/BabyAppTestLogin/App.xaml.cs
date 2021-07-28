
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BabyAppTestLogin.Services;
//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BabyAppTestLogin
{
    public partial class App : Application
    {
        public static string ApiEndpoint = "https://graph.microsoft.com/v1.0/me";


        public App()
        {
            InitializeComponent();

            DependencyService.Register<BAservice>();

            MainPage = new NavigationPage(new MainPage());
           
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
