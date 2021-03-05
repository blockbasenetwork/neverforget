namespace BlockBase.Dapps.NeverForgetBot.Services.API.Models
{
    public class RedditAccessTokenResultModel
    {
        public RedditAccessTokenModel[] Data { get; set; }
    }

    public class RedditAccessTokenModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }

    }
}
