using System;
using System.Web.Mvc;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using Google.Apis.Util.Store;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;

namespace AdAtTheRightTime.Tools
{
    public class AppFlowMetadata : FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow flow =
               new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
               {
                   ClientSecrets = new ClientSecrets
                   {
                       ClientId = "285702963273-qqvr2ht85lq3r20shh49bqqbhbbpvno3.apps.googleusercontent.com",
                       ClientSecret = "PMSD4oy-G6hHu62d_0D0vM25"
                   },
                   Scopes = new[] { DriveService.Scope.Drive },
                   DataStore = new FileDataStore("Drive.Api.Auth.Store")
               });

        public override string GetUserId(Controller controller)
        {
            // In this sample we use the session to store the user identifiers.
            // That's not the best practice, because you should have a logic to identify
            // a user. You might want to use "OpenID Connect".
            // You can read more about the protocol in the following link:
            // https://developers.google.com/accounts/docs/OAuth2Login.
            var user = controller.Session["user"];
            if (user == null)
            {
                user = Guid.NewGuid();
                controller.Session["user"] = user;
            }
            return user.ToString();

        }

        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }
}