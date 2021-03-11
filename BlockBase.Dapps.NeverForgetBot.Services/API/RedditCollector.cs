using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class RedditCollector
    {
        public async Task<RedditContextModel[]> RedditContextInfo()
        {
            string url = "https://api.pushshift.io/reddit/comment/search/?q=%21neverforget&subreddit=test";

            var result = await ApiHelper.FetchDataFromReddit<RedditContextResultModel>(url);
            return result.Data;
        }

        public async Task<RedditCommentModel[]> RedditCommentInfo()
        {
            string url = "https://api.pushshift.io/reddit/comment/search/?q=%21neverforget&subreddit=test";

            var result = await ApiHelper.FetchDataFromReddit<RedditCommentResultModel>(url);
            return result.Data;
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
            com.Reply($"@{ comment.Author } Never Forget { url } ");
        }
    }
}

