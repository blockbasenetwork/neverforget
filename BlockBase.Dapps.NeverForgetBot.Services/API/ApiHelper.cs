using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<T> FetchDataFromReddit<T>(string url)
        {
            using (HttpResponseMessage response = await ApiClient.GetAsync(url))
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

        public static void InitializeRedditClient()
        {
            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Username", Resources.RedditTokens.APP_ID);
            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Password", Resources.RedditTokens.SECRET);
        }

        public static async Task<T> FetchDataFromReddit<T>(HttpRequestMessage message)
        {
            using (HttpResponseMessage response = await ApiClient.SendAsync(message))
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
