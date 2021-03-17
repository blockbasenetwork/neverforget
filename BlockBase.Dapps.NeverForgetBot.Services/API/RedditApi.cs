using Reddit.AuthTokenRetriever;
using RedditSharp;
using System.Diagnostics;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class RedditApi
    {
        public static RedditSharp.Reddit Client { get; set; }

        public static void AuthenticateClient()
        {
            var webAgent = new BotWebAgent(Resources.RedditTokens.USERNAME, Resources.RedditTokens.PASSWORD, Resources.RedditTokens.APP_ID, Resources.RedditTokens.SECRET, Resources.RedditTokens.REDIRECT_URI);
            Client = new RedditSharp.Reddit(webAgent, false);
        }

        #region AcessAuth
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
        #endregion
    }
}