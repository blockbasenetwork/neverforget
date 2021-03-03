using BlockBase.Dapps.NeverForgetBot.Services.Resources;
using Tweetinvi;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class TwitterApi
    {
        public static TwitterClient Client { get; set; }

        public static void AuthenticateClient()
        {
            Client = new TwitterClient(TwitterTokens.CONSUMER_KEY, TwitterTokens.CONSUMER_SECRET, TwitterTokens.ACCESS_TOKEN, TwitterTokens.ACCESS_TOKEN_SECRET);
        }
    }
}