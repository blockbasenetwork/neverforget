using BlockBase.Dapps.NeverForget.Services.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Tweetinvi;

namespace BlockBase.Dapps.NeverForget.Services.API
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
