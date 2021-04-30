using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.DataAccess.Interfaces
{
    public interface IRedditContextDataAccessObject : IBaseDataAccessObject<RedditContext>
    {
        Task<List<RedditCommentModel>> GetUniqueComments(RedditCommentModel[] commentArray);

        Task<bool> IsContextPresent(Guid contextId);
        Task<bool> IsCommentPresent(Guid contextId);
        Task<bool> IsSubmissionPresent(Guid contextId);
    }
}
