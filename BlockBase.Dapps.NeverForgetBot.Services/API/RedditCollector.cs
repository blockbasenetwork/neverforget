using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class RedditCollector
    {
        public async Task<RedditModel[]> RedditInfo()
        {
            string url = "https://api.pushshift.io/reddit/comment/search/?q=!neverforgetbot";

            var result = await FetchDataFromReddit<RedditResultModel>(url);
            return result.Data;
        }

        public async Task<RedditModel[]> RedditCommentIdInfo(string id)
        {
            string url = "https://api.pushshift.io/reddit/comment/search/?ids=" + id;

            var result = await FetchDataFromReddit<RedditResultModel>(url);
            return result.Data;
        }

        public async Task<RedditModel[]> RedditPostIdInfo(string id)
        {
            string url = "https://api.pushshift.io/reddit/submission/search/?ids=" + id;

            var result = await FetchDataFromReddit<RedditResultModel>(url);
            return result.Data;
        }

        public async Task<T> FetchDataFromReddit<T>(string url)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<T>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
