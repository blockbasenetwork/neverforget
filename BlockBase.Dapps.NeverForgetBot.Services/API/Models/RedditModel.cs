//using BlockBase.Dapps.NeverForgetBot.Business.BusinessModels;

namespace BlockBase.Dapps.NeverForgetBot.Services.API.Models
{
    public class RedditResultModel
    {
        public RedditModel[] Data { get; set; }
    }

    public class RedditModel
    {
        public string Author { get; set; }
        public string Body { get; set; }
        public int Created_Utc { get; set; }
        public string Id { get; set; }
        public string SubReddit { get; set; }
        public string Parent_Id { get; set; }
    }
}
