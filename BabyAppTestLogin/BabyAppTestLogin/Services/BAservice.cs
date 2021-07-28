using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BabyAppTestLogin.Views;
using Xamarin.Essentials;
using System.Diagnostics;

namespace BabyAppTestLogin.Services
{
    public class BAservice
    {
        private readonly IPublicClientApplication _pca;

        private static readonly Lazy<BAservice> lazy = new Lazy<BAservice>
           (() => new BAservice());

        public static BAservice Instance { get { return lazy.Value; } }

        public object Parent { get; private set; }

        public BAservice()
        {
            //constructor
            _pca = PublicClientApplicationBuilder.Create(BAconstants.ClientID)
                           .WithIosKeychainSecurityGroup(BAconstants.AppId)
                           .WithRedirectUri(BAconstants.RedirectURI)
                           .WithAuthority("https://login.microsoftonline.com/common")
                           .Build();
   
        }


        public async Task<bool> SignInAsync()
        {
            try
            {
                //the user has already signed in 
                var accounts = await _pca.GetAccountsAsync(); //0
                var firstAccount = accounts.FirstOrDefault();
                var authResult = await _pca.AcquireTokenSilent(BAconstants.Scopes, firstAccount).ExecuteAsync();

                // Store the access token securely for later use.
                await SecureStorage.SetAsync("AccessToken", authResult?.AccessToken);

                return true;
            }
            catch (MsalUiRequiredException)
            {
                try
                {
                    // This means we need to login again through the MSAL window.
                    
                   var authResult = await _pca.AcquireTokenInteractive(BAconstants.Scopes)
                      .WithAuthority("https://login.microsoftonline.com/496a4ab1-caef-478f-938d-6551aca7fb85/oauth2/v2.0/authorize")
                      .WithParentActivityOrWindow(BAconstants.ParentWindow)
                      .ExecuteAsync();

                    // Store the access token securely for later use.
                    await SecureStorage.SetAsync("AccessToken", authResult?.AccessToken);

                    return true;
                }
                catch (Exception ex2)
                {
                    Debug.WriteLine(ex2.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }

 
        private IAccount GetAccountByPolicy(IEnumerable<IAccount> accounts, string policy)
        {
            foreach (var account in accounts)
            {
                string userIdentifier = account.HomeAccountId.ObjectId.Split('.')[0];
                if (userIdentifier.EndsWith(policy.ToLower())) return account;
            }

            return null;
        }
       
   
        public async Task<bool> SignOutAsync()
        {
 
                try
                {
                    var accounts = await _pca.GetAccountsAsync();

                    // Go through all accounts and remove them.
                    while (accounts.Any())
                    {
                        await _pca.RemoveAsync(accounts.FirstOrDefault());
                        accounts = await _pca.GetAccountsAsync();
                    }

                    // Clear our access token from secure storage.
                    SecureStorage.Remove("AccessToken");

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return false;
                }
           
        }

    }
}
