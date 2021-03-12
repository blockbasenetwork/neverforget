using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class RedditCollector
    {
        public async Task<RedditCommentModel[]> RedditCommentInfo()
        {

            var lastDate = ReadLastCommentDate();

            string url = $"https://api.pushshift.io/reddit/comment/search/?q=%21neverforget&size=100&after={lastDate}";

            var result = await ApiHelper.FetchDataFromReddit<RedditCommentResultModel>(url);


            if (result.Data.Length != 0)
            {
                var lastComment = result.Data[^1];
                CreateLastCommentDate(lastComment.Created_Utc);
            }

            return result.Data;
        }

        public RedditCommentModel[] IterateCommentList()
        {
            List<RedditCommentModel> result = new List<RedditCommentModel>();

            while (RedditCommentInfo().Result.Length != 0)
            {
                foreach (var item in RedditCommentInfo().Result)
                {
                    result.Add(item);
                }
            }
            return result.ToArray();

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

        public void PublishUrl(string url, RedditComment comment)
        {
            var com = RedditApi.Client.Comment($"t1_{comment.CommentId}");
            com.Reply($"@{ comment.Author } { url } ");
        }
    }
}

