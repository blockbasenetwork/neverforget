namespace BlockBase.Dapps.NeverForgetBot.Services.API.Models
{
    public class RedditContextResultModel
    {
        public RedditContextModel[] Data { get; set; }
    }

    public class RedditContextModel
    {
        public string Body { get; set; }
        public string Id { get; set; }
        public string Parent_Id { get; set; }
        public string Link_Id { get; set; }
    }
}
