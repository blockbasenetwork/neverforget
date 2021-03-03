using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Tweetinvi.Exceptions;
using Tweetinvi.Parameters;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class TwitterCollector
    {
        /*
        public async Task<TweetModel> GetTweet(string id)
        {
            try
            {
                var result = await TwitterApi.Client.Tweets.GetTweetAsync(long.Parse(id));
                TweetModel tweet = JsonConvert.DeserializeObject<TweetModel>(TwitterApi.Client.Json.Serialize(result));

                return tweet;
            }
            catch (TwitterException e)
            {
                throw e;
            }
        }

        public async Task<TweetModel[]> GetMentions()
        {
            try
            {
                List<TweetModel> mentionList = new List<TweetModel>();

                var result = await TwitterApi.Client.Timelines.GetMentionsTimelineAsync();
                //string jsonResult = TwitterApi.Client.Json.Serialize(result);

                mentionList = JsonConvert.DeserializeObject<List<TweetModel>>(TwitterApi.Client.Json.Serialize(result));

                
                //foreach (var mention in result)
                //{
                //    mentionList = JsonConvert.DeserializeObject<List<TweetModel>>(JsonConvert.SerializeObject(mention)));
                //}
                

                return mentionList.ToArray();
            }
            catch (TwitterException e)
            {
                throw e;
            }
        }*/


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
                if(e.StatusCode == 404)
                {
                    return new TweetModel();
                }
                throw e;
            }
        }

        public async Task<TweetModel> GetParentFrom(string id)
        {
            try
            {
                var result = await TwitterApi.Client.Tweets.GetTweetAsync(long.Parse(id));
                string jsonResult = TwitterApi.Client.Json.Serialize(result);
                TweetModel tweet = JsonConvert.DeserializeObject<TweetModel>(jsonResult);

                return tweet;
            }
            catch (TwitterException e)
            {
                throw e;
            }
        }

        /*public async Task<TweetModel> GetSubmissionFromTweet(string id)
        {
            try
            {
                var result = await TwitterApi.Client.Tweets.GetTweetAsync(long.Parse(id));
                string jsonResult = TwitterApi.Client.Json.Serialize(result);
                TweetModel tweet = JsonConvert.DeserializeObject<TweetModel>(jsonResult);

                if(tweet.In_reply_to_status_id_str != null)
                {
                    return await GetSubmissionFromTweet(tweet.In_reply_to_status_id_str);
                }

                return tweet;
            }
            catch (TwitterException e)
            {
                throw e;
            }
        }*/


        public async Task<string> GetTweetJson(string id)
        {
            try
            {
                var result = await TwitterApi.Client.Tweets.GetTweetAsync(long.Parse(id));

                return TwitterApi.Client.Json.Serialize(result);
            }
            catch (TwitterException e)
            {
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
                var reply = await TwitterApi.Client.Tweets.PublishTweetAsync(new PublishTweetParameters("@" + tweet.CreatedBy + " - There is a deleted tweet in the thread. Call me in another tweet.")
                {
                    InReplyToTweet = tweet
                });
            }
            catch(TwitterException e)
            {
                if(e.StatusCode == 403)
                {
                    Console.WriteLine("ola");
                }
                throw e;
            }
        }

        //public async Task PublishUrl(string url, string contextId)
        //{

        //    var tweet = await TwitterApi.Client.Tweets.GetTweetAsync(long.Parse(contextId));
        //    var reply = await TwitterApi.Client.Tweets.PublishTweetAsync(new PublishTweetParameters("@" + tweet.CreatedBy + " Never Forget " + url)
        //    {
        //        InReplyToTweet = tweet
        //    });

        //    // remove the same way as you would delete a tweet
        //    //await TwitterApi.Client.Tweets.DestroyTweetAsync(reply);
        //}


    }
}