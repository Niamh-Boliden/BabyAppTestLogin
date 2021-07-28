using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BabyAppTestLogin.Services;
using BabyAppTestLogin.Views;
using System.Net.Http;
using Microsoft.Identity.Client;


namespace BabyAppTestLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly BAservice _authService;
        private readonly SimpleGraphService _simpleGraphService;
        public bool IsSignedIn { get; set; }
        public bool IsSigningIn { get; set; }
        public string Name { get; set; }
        public MainPage()
        {
            InitializeComponent();
            _authService = new BAservice();
            _simpleGraphService = new SimpleGraphService();
        }
        async void OnSignInSignOut(object sender, EventArgs e)
        {
            
            try
            {
                if (btnSignInSignOut.Text == "Sign in")
                {
                  
                 //  IsSigningIn = true;


                    if (await _authService.SignInAsync())
                    {
                        UserContext newUser = await _simpleGraphService.GetUserAsync();
                        lblName.Text = newUser.GivenName;
                        lblJob.Text = newUser.JobTitle;
                        IsSignedIn = true;
                        //  UpdateUserInfo();
                    }

                  //  IsSigningIn = false;
                }
                else
                {
                    if (await _authService.SignOutAsync())
                    {
                        IsSignedIn = false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Checking the exception message 
                // should ONLY be done for B2C
                // reset and not any other error.
                //if (ex.Message.Contains("AADB2C90118"))
                //    OnPasswordReset();
                //Alert if any exception excluding user canceling sign -in dialog
                //else if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
                    await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }

            //void UpdateSignInState(UserContext userContext)
            //{
            //    var isSignedIn = userContext.IsLoggedOn;
            //    btnSignInSignOut.Text = isSignedIn ? "Sign out" : "Sign in";
               
            //    //btnCallApi.IsVisible = isSignedIn;
            //    slUser.IsVisible = isSignedIn;
            //    lblApi.Text = "";
            //}
            //void UpdateUserInfo(UserContext userContext)
            //{
            //    lblName.Text = userContext.Name;
            //    lblJob.Text = userContext.JobTitle;
            //    lblCity.Text = userContext.City;
            //}
        }
        //async void OnCallApi(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblApi.Text = $"Calling API {App.ApiEndpoint}";
        //        var userContext = await BAservice.Instance.SignInAsync();
        //        var token = userContext.AccessToken;

        //        // Get data from API
        //        HttpClient client = new HttpClient();
        //        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, App.ApiEndpoint);
        //        //??
        //        message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //        HttpResponseMessage response = await client.SendAsync(message);
        //        string responseString = await response.Content.ReadAsStringAsync();
        //        if (response.IsSuccessStatusCode)
        //        {
        //            lblApi.Text = $"Response from API {App.ApiEndpoint} | {responseString}";
        //        }
        //        else
        //        {
        //            lblApi.Text = $"Error calling API {App.ApiEndpoint} | {responseString}";
        //        }
        //    }
        //    catch (MsalUiRequiredException ex)
        //    {
        //        await DisplayAlert($"Session has expired, please sign out and back in.", ex.ToString(), "Dismiss");
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
        //    }
        //}
    }
}