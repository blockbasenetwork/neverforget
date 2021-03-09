using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using Newtonsoft.Json;
using Reddit;
using Reddit.AuthTokenRetriever;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class RedditApi
    {
        public static void AuthenticateClient()
        {
            var rToken = GetTokens();
            rToken.access_token = "776561914738" + rToken.access_token;
            rToken.refresh_token = "776561914738" + rToken.refresh_token;

            var user_agent = "NeverForgetBot1.0";

            var reddit = new RedditClient(Resources.RedditTokens.APP_ID_script, rToken.refresh_token, Resources.RedditTokens.SECRET_script, rToken.access_token, user_agent);
            Console.WriteLine("Username: " + reddit.Account.Me.Name);
            Console.WriteLine("Cake Day: " + reddit.Account.Me.Created.ToString("D"));

            //List<Post> posts = reddit.Subreddit("MySub").Search(new SearchGetSearchInput("Bernie Sanders"));  // Search r/MySub
            //if (posts.Count == 0)
            //{
            //    posts = reddit.Subreddit("all").Search(new SearchGetSearchInput("Bernie Sanders"));  // Search r/all
            //}
        }

        public static RedditAccessTokenModel GetTokens()
        {
            String encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(Resources.RedditTokens.APP_ID_script + ":" + Resources.RedditTokens.SECRET_script));

            var request = (HttpWebRequest)WebRequest.Create("https://www.reddit.com/api/v1/access_token?grant_type=client_credentials&duration=permanent");

            request.Headers.Add("Authorization", "Basic " + encoded);
            request.Method = "POST";
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var result = JsonConvert.DeserializeObject<RedditAccessTokenModel>(responseString);


            return result;
        }

        public static string AuthorizeUser()
        {
            AuthTokenRetrieverLib authTokenRetrieverLib = new AuthTokenRetrieverLib(Resources.RedditTokens.APP_ID_script, Resources.RedditTokens.SECRET_script, 8080);

            authTokenRetrieverLib.AwaitCallback();

            OpenBrowser(authTokenRetrieverLib.AuthURL());

            while (true) { }

            authTokenRetrieverLib.StopListening();

            return authTokenRetrieverLib.RefreshToken;
        }

        public static void OpenBrowser(string authUrl)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(authUrl);
                Process.Start(processStartInfo);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe")
                {
                    Arguments = authUrl
                };
                Process.Start(processStartInfo);
            }
        }
    }
}