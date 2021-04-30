using RedditSharp;

namespace BlockBase.Dapps.NeverForget.Services.API
{
    public class RedditApi
    {
        public static Reddit Client { get; set; }

        public static void AuthenticateClient()
        {
            var webAgent = new BotWebAgent(Resources.RedditTokens.USERNAME, Resources.RedditTokens.PASSWORD, Resources.RedditTokens.APP_ID, Resources.RedditTokens.SECRET, Resources.RedditTokens.REDIRECT_URI);
            Client = new RedditSharp.Reddit(webAgent, false);
        }
    }
}
