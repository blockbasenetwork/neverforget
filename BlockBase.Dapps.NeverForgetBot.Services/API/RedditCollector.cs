using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class RedditCollector
    {
        public static async Task<RedditModel[]> RedditInfo()
        {
            string url = "https://api.pushshift.io/reddit/comment/search/?q=neverforgetbot";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<RedditResultModel>();
                    return result.Data;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
