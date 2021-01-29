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

        //public static RedditModel FromBusiness(RedditContextBusinessModel redditContextBusinessModel)
        //{
        //    return new RedditModel()
        //    {
        //        Author = redditContextBusinessModel.Author,
        //        Body = redditContextBusinessModel.CommentPost,
        //        Created_Utc = redditContextBusinessModel.PostingDate,
        //        Id = redditContextBusinessModel.CommentId,
        //        SubReddit = redditContextBusinessModel.SubReddit
        //    };
        //}

        //public RedditContextBusinessModel ToBusiness()
        //{
        //    return new RedditContextBusinessModel()
        //    {
        //        Author = Author,
        //        CommentPost = Body,
        //        PostingDate = Created_Utc,
        //        CommentId = Id,
        //        SubReddit = SubReddit
        //    };
        //}
    }   
}
