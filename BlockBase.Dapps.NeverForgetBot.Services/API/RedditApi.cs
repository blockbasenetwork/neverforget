using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using Reddit;
using Reddit.Controllers;
using Reddit.Inputs.Search;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class RedditApi
    {
        public static void AuthenticateClient()
        {
            //string refreshToken = AuthorizeUser();

            var reddit = new RedditClient(Resources.RedditTokens.APP_ID, GetRefreshToken().Result);

            List<Post> posts = reddit.Subreddit("MySub").Search(new SearchGetSearchInput("Bernie Sanders"));  // Search r/MySub
            if (posts.Count == 0)
            {
                posts = reddit.Subreddit("all").Search(new SearchGetSearchInput("Bernie Sanders"));  // Search r/all
            }
        }

        public static async Task<string> GetRefreshToken()
        {
            var urlRT = "https://www.reddit.com/api/v1/access_token";
            var parameters = new Dictionary<string, string> { { "grant_type", "client_credentials" }, { "duration", "permanent" } };
            var encodedContent = new FormUrlEncodedContent(parameters);

            ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Username", Resources.RedditTokens.APP_ID);
            ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Password", Resources.RedditTokens.SECRET);

            var retrievedToken = await ApiHelper.FetchDataFromReddit<RedditAccessTokenModel>(urlRT, encodedContent);
            var result = retrievedToken.refresh_token;
            return result;
        }

        //public static string AuthorizeUser()
        //{
        //    // Create a new instance of the auth token retrieval library.  --Kris
        //    AuthTokenRetrieverLib authTokenRetrieverLib = new AuthTokenRetrieverLib(Resources.RedditTokens.APP_ID, Resources.RedditTokens.SECRET, 8080);

        //    // Start the callback listener.  --Kris
        //    // Note - Ignore the logging exception message if you see it.  You can use Console.Clear() after this call to get rid of it if you're running a console app.
        //    authTokenRetrieverLib.AwaitCallback();

        //    // Open the browser to the Reddit authentication page.  Once the user clicks "accept", Reddit will redirect the browser to localhost:8080, where AwaitCallback will take over.  --Kris
        //    OpenBrowser(authTokenRetrieverLib.AuthURL());

        //    // Replace this with whatever you want the app to do while it waits for the user to load the auth page and click Accept.  --Kris
        //    while (true) { }
        //    //Console.WriteLine("Hello world!");
        //    //Console.WriteLine(authTokenRetrieverLib.RefreshToken);

        //    // Cleanup.  --Kris
        //    authTokenRetrieverLib.StopListening();

        //    return authTokenRetrieverLib.RefreshToken;
        //}

        //public static void OpenBrowser(string authUrl = "https://www.reddit.com/api/v1/authorize?client_id=z_FR2DzDnW3EEw&response_type=code&state=z_FR2DzDnW3EEw:B4zij4E3RceAdV_maxlHzuUW3lC9kg&redirect_url=http://localhost:8080/Reddit.NET/oauthRedirect&scope=submit", string browserPath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe")
        //{
        //    //string authUrl = "https://www.reddit.com/api/v1/authorize?client_id=" + "z_FR2DzDnW3EEw" + "&response_type=code"
        //    //    + "&state=" + "z_FR2DzDnW3EEw" + ":" + "B4zij4E3RceAdV_maxlHzuUW3lC9kg"
        //    //    + "&redirect_url=http://localhost:8080/Reddit.NET/oauthRedirect"
        //    //    + "&scope=submit";

        //    //string browserPath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"

        //    try
        //    {
        //        ProcessStartInfo processStartInfo = new ProcessStartInfo(authUrl);
        //        Process.Start(processStartInfo);
        //    }
        //    catch (System.ComponentModel.Win32Exception)
        //    {
        //        // This typically occurs if the runtime doesn't know where your browser is.  Use BrowserPath for when this happens.  --Kris
        //        ProcessStartInfo processStartInfo = new ProcessStartInfo(browserPath)
        //        {
        //            Arguments = authUrl
        //        };
        //        Process.Start(processStartInfo);
        //    }
        //}
    }
}