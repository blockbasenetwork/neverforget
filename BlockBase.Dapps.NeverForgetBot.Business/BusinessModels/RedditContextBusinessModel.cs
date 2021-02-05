using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessModels
{
    public class RedditContextBusinessModel
    {
        public Guid Id { get; set; }
        public int RequestTypeId { get; set; }
        public RequestTypeEnum RequestType { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public static RedditContextBusinessModel FromData(RedditContext redditContext)
        {
            return new RedditContextBusinessModel()
            {
                Id = redditContext.Id,
                RequestTypeId = redditContext.RequestTypeId,
                RequestType = (RequestTypeEnum)redditContext.RequestTypeId,
                CreatedAt = redditContext.CreatedAt,
                IsDeleted = redditContext.IsDeleted,
                DeletedAt = redditContext.DeletedAt

            };
        }

        public RedditContext ToData()
        {
            return new RedditContext()
            {
                Id = Id,
                RequestTypeId = RequestTypeId,
                CreatedAt = CreatedAt,
                IsDeleted = IsDeleted,
                DeletedAt = DeletedAt
            };
        }
    }
}
