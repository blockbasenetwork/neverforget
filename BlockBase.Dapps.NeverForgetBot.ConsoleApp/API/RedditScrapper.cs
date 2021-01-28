using BlockBase.Dapps.NeverForgetBot.ConsoleApp.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.ConsoleApp.API
{
    public class RedditScrapper
    {

        public static async Task<RedditModel[]> RedditInfo()
        {
            string url = "https://api.pushshift.io/reddit/comment/search/?&q=neverforgetbot";

            using(HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
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
