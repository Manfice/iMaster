﻿using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Domen.infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using Web.Models;

namespace Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(Context.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                },
                ExpireTimeSpan = TimeSpan.FromDays(2),
                SlidingExpiration = true
                
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            var fbOptions = new FacebookAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Passive,
                CallbackPath = new PathString("/FbLoginCallback"),
                AppId = "1529850407031049",
                AppSecret = "b8fdb276d60f5ec2fcfa4deab48a3cfc",
                Caption = "iMaster",
                AuthenticationType = "Facebook",
                Provider = new FacebookAuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new Claim("FacebookAccessToken", context.AccessToken, "Facebook"));
                        return Task.FromResult(0);
                    } 
                },
                Scope = {"email", "user_friends"},
                BackchannelTimeout = TimeSpan.FromSeconds(60),
                SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType(),
                 
            };
            app.UseFacebookAuthentication(fbOptions);

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            var googleOptions = new GoogleOAuth2AuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Passive,
                AuthenticationType = "Google",
                ClientId = "205784610999-tdgrkpqapebosucomstkbov1tm0n0dca.apps.googleusercontent.com",
                ClientSecret = "Hq0T7u0EkekuziBYz7mlmPMY",
                CallbackPath = new PathString("/GoogleOAuthcallback"),
                SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType(),
                BackchannelTimeout = TimeSpan.FromSeconds(60),
                BackchannelHttpHandler = new WebRequestHandler(),
                BackchannelCertificateValidator = null,
                Provider = new GoogleOAuth2AuthenticationProvider()
            };
            app.UseGoogleAuthentication(googleOptions);
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}