using System;
using System.Collections.Generic;
using System.Text;

namespace BabyAppTestLogin.Views
{
     public class BAconstants
    {
        // Azure AD B2C Coordinates
        public static string Tenant = "496a4ab1-caef-478f-938d-6551aca7fb85"; //??
        public static string AzureADB2CHostname = "boliden.onmicrosoft.com"; //??
        public static string ClientID = "0ed9265f-f44c-40da-9a29-7c6c58602ec6";
    
        public static string AppId = "com.companyname.babyapptestlogin";
       // public static string RedirectURI = "msauth://com.companyname.babyapptestlogin/hJ7eQEkJaGHLvxF1hjtWrrkZJpY%3D";
        public static string RedirectURI = "msal0ed9265f-f44c-40da-9a29-7c6c58602ec6://auth";

        public static string[] Scopes = { "User.Read" };
        public static object ParentWindow { get; set; }

        public static string IOSKeyChainGroup = "com.microsoft.adalcache";
    }
}
