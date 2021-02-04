namespace BlockBase.Dapps.NeverForgetBot.Services.API.Models
{
    public class RedditCommentResultModel
    {
        public RedditCommentModel[] Data { get; set; }
    }

    public class RedditCommentModel
    {
        public string Author { get; set; }
        public string Body { get; set; }
        public int Created_Utc { get; set; }
        public string Id { get; set; }
        public string SubReddit { get; set; }
        public string Parent_Id { get; set; }
        public string Link_Id { get; set; }
    }
}
