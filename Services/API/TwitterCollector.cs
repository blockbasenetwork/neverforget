using BlockBase.Dapps.NeverForget.Services.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Exceptions;
using Tweetinvi.Parameters;

namespace BlockBase.Dapps.NeverForget.Services.API
{
    public class TwitterCollector
    {
        public async Task<TweetModel> GetTweet(string id)
        {
            try
            {
                var result = await TwitterApi.Client.Tweets.GetTweetAsync(long.Parse(id));

                string jsonResult = TwitterApi.Client.Json.Serialize(result);
                var tweet = JsonConvert.DeserializeObject<TweetModel>(jsonResult);

                return tweet;
            }
            catch (TwitterException e)
            {
                if (e.StatusCode == 404)
                {
                    return new TweetModel();
                }
                throw e;
            }
        }

        public async Task<TweetModel[]> GetMentions()
        {
            try
            {
                var result = await TwitterApi.Client.Timelines.GetMentionsTimelineAsync();
                string jsonResult = TwitterApi.Client.Json.Serialize(result);
                TweetModel[] mentionList = JsonConvert.DeserializeObject<TweetModel[]>(jsonResult);

                return mentionList;
            }
            catch (TwitterException e)
            {
                throw e;
            }
        }

        public async Task ReplyWithError(string contextId)
        {
            try
            {
                var tweet = await TwitterApi.Client.Tweets.GetTweetAsync(long.Parse(contextId));
                var reply = await TwitterApi.Client.Tweets.PublishTweetAsync(new PublishTweetParameters($"There is a deleted tweet in the thread. Call me in another tweet.")
                {
                    InReplyToTweet = tweet
                });
            }
            catch (TwitterException e)
            {
                if (e.StatusCode == 403)
                {
                    Console.WriteLine("Exception --> TwitterCollector.cs -- Duplicated Tweet [ReplyWithError()]\n");
                }
                throw e;
            }
        }

        public async Task ReplyWithUnable(string contextId)
        {
            try
            {
                var tweet = await TwitterApi.Client.Tweets.GetTweetAsync(long.Parse(contextId));
                var reply = await TwitterApi.Client.Tweets.PublishTweetAsync(new PublishTweetParameters($"There is nothing above to save. Call me in another tweet.")
                {
                    InReplyToTweet = tweet
                });
            }
            catch (TwitterException e)
            {
                if (e.StatusCode == 403)
                {
                    Console.WriteLine("Exception --> TwitterCollector.cs -- Duplicated Tweet [ReplyWithUnable()]\n");
                }
                throw e;
            }
        }

        public async Task PublishUrl(string url, string contextId)
        {
            try
            {
                var tweet = await TwitterApi.Client.Tweets.GetTweetAsync(long.Parse(contextId));
                var reply = await TwitterApi.Client.Tweets.PublishTweetAsync(new PublishTweetParameters($"Never Forget { url }")
                {
                    InReplyToTweet = tweet
                });
            }
            catch (TwitterException e)
            {
                if (e.StatusCode == 403)
                {
                    Console.WriteLine("Exception --> TwitterCollector.cs -- Duplicated Tweet [PublishUrl()]\n");
                }
                throw e;
            }
        }
    }
}
