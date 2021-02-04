namespace BlockBase.Dapps.NeverForgetBot.Services.API.Models
{
    public class RedditSubmissionResultModel
    {
        public RedditSubmissionModel[] Data { get; set; }
    }

    public class RedditSubmissionModel
    {
        public string Author { get; set; }
        public string Body { get; set; }
        public int Created_Utc { get; set; }
        public string Id { get; set; }
        public string SubReddit { get; set; }
        public string Title { get; set; }
    }
}
