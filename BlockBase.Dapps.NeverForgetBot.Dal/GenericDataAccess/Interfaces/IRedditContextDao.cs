using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces
{
    public interface IRedditContextDao : IBaseAuditDao<RedditContext, Guid>
    {
        Task<List<RedditCommentModel>> GetUniqueComments(RedditCommentModel[] commentArray);
    }
}
