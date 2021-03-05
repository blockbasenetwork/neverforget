using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using BlockBase.Dapps.NeverForgetBot.Services.API;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class RedditCollector
    {
        public async Task<RedditContextModel[]> RedditContextInfo()
        {
            string url = "https://api.pushshift.io/reddit/comment/search/?q=never";

            var result = await FetchDataFromReddit<RedditContextResultModel>(url);
            return result.Data;
        }

        public async Task<RedditCommentModel[]> RedditCommentInfo()
        {
            string url = "https://api.pushshift.io/reddit/comment/search/?q=never";

            var result = await FetchDataFromReddit<RedditCommentResultModel>(url);
            return result.Data;
        }

        public async Task<RedditCommentModel[]> RedditParentCommentInfo(string id)
        {
            string url = "https://api.pushshift.io/reddit/comment/search/?ids=" + id;

            var result = await FetchDataFromReddit<RedditCommentResultModel>(url);
            return result.Data;
        }

        public async Task<RedditSubmissionModel[]> RedditSubmissionInfo(string id)
        {
            string url = "https://api.pushshift.io/reddit/submission/search/?ids=" + id;

            var result = await FetchDataFromReddit<RedditSubmissionResultModel>(url);
            return result.Data;
        }

        
    }
}
