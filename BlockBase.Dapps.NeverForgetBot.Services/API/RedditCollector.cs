using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedditSharp.Things;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class RedditCollector
    {
        public async Task<RedditCommentModel[]> RedditCommentInfo(int lastDate)
        {
            string url = $"https://api.pushshift.io/reddit/comment/search/?q=%21neverforget&size=100&subreddit=test&after={lastDate}"; //change before deploy
            var result = await ApiHelper.FetchDataFromReddit<RedditCommentResultModel>(url);
            return result.Data;
        }

        public void CreateLastCommentDate(int lastCommentDate)
        {
            var lastDateJson = JsonConvert.SerializeObject(new
            {
                lastDate = lastCommentDate.ToString()
            });

            string fileName = "lastDateReddit.json";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, lastDateJson);
            }
        }

        public int ReadLastCommentDate()
        {
            string fileName = "lastDateReddit.json";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

            using (StreamReader file = File.OpenText(filePath))
            {
                var json = file.ReadToEnd();

                var jsonString = JsonConvert.DeserializeObject<string>(json);
                JObject obj = JObject.Parse(jsonString);
                JToken token = obj["lastDate"];
                int lastDate = (int)token;

                return lastDate;
            }
        }

        public async Task<RedditCommentModel[]> RedditParentCommentInfo(string id)
        {
            string url = "https://api.pushshift.io/reddit/comment/search/?ids=" + id;

            var result = await ApiHelper.FetchDataFromReddit<RedditCommentResultModel>(url);
            return result.Data;
        }

        public async Task<RedditSubmissionModel[]> RedditSubmissionInfo(string id)
        {
            string url = "https://api.pushshift.io/reddit/submission/search/?ids=" + id;

            var result = await ApiHelper.FetchDataFromReddit<RedditSubmissionResultModel>(url);
            return result.Data;
        }

        public void PublishUrl(string url, string commentId)
        {
            Comment com = (Comment)RedditApi.Client.GetThingByFullnameAsync($"t1_{commentId}").Result;
            com.ReplyAsync($"@NeverForget-Bot { url } ");
            Thread.Sleep(60000);
        }
    }
}

