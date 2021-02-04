using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessModels
{
    public class TwitterContextBusinessModel
    {
        public Guid Id { get; set; }
        public Guid RequestTypeId { get; set; }
        //public RequestType RequestType { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public static TwitterContextBusinessModel FromData(TwitterContext twitterContext)
        {
            return new TwitterContextBusinessModel()
            {
                Id = twitterContext.Id,
                //RequestTypeId = twitterContext.RequestTypeId,
                //RequestType = twitterContext.RequestType,
                CreatedAt = twitterContext.CreatedAt,
                IsDeleted = twitterContext.IsDeleted,
                DeletedAt = twitterContext.DeletedAt
            };
        }

        public TwitterContext ToData()
        {
            return new TwitterContext()
            {
                Id = Id,
                //RequestTypeId = RequestTypeId,
                //RequestType = RequestType,
                CreatedAt = CreatedAt,
                IsDeleted = IsDeleted,
                DeletedAt = DeletedAt
            };
        }
    }
}

