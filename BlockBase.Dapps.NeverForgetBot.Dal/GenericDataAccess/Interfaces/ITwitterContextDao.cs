using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces
{
    public interface ITwitterContextDao : IBaseAuditDao<TwitterContext, Guid>
    {
        Task<List<TweetModel>> GetUniqueComments(TweetModel[] tweetList);
        Task<bool> IsContextPresent(Guid contextId);
        Task<bool> IsCommentPresent(Guid contextId);
        Task<bool> IsSubmissionPresent(Guid contextId);
    }
}
