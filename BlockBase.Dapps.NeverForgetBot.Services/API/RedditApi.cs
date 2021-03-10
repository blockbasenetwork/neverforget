using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using Newtonsoft.Json;
using Reddit;
using Reddit.AuthTokenRetriever;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class RedditApi
    {
        public static RedditClient Client { get; set; }

        public static void AuthenticateClient()
        {
            var rTokens = GetTokens();
            var prefix = Regex.Match(rTokens.access_token, @"^.*?(?=-)");
            var refreshToken = prefix + GetRefreshToken();


            Client = new RedditClient(Resources.RedditTokens.APP_ID, refreshToken, Resources.RedditTokens.SECRET, rTokens.access_token, Resources.RedditTokens.USER_AGENT);
        }


        public static RedditAccessTokenModel GetTokens()
        {
            var encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(Resources.RedditTokens.APP_ID + ":" + Resources.RedditTokens.SECRET));
            var client = new RestClient("https://www.reddit.com/api/v1/access_token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Basic {encoded}");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("User-Agent", Resources.RedditTokens.USER_AGENT);
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", Resources.RedditTokens.USERNAME);
            request.AddParameter("password", Resources.RedditTokens.PASSWORD);

            var response = client.Execute(request);
            var responseString = response.Content;
            var result = JsonConvert.DeserializeObject<RedditAccessTokenModel>(responseString);

            return result;
        }

        public static string GetRefreshToken()
        {
            String encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(Resources.RedditTokens.APP_ID + ":" + Resources.RedditTokens.SECRET));

            var request = (HttpWebRequest)WebRequest.Create("https://www.reddit.com/api/v1/access_token?grant_type=client_credentials&duration=permanent");

            request.Headers.Add("Authorization", "Basic " + encoded);
            request.Method = "POST";
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var result = JsonConvert.DeserializeObject<RedditAccessTokenModel>(responseString);


            return result.refresh_token;
        }

        public static string AuthorizeUser()
        {
            AuthTokenRetrieverLib authTokenRetrieverLib = new AuthTokenRetrieverLib(Resources.RedditTokens.APP_ID, Resources.RedditTokens.SECRET, 8080);

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