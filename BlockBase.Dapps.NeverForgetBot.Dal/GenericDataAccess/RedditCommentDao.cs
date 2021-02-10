using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess
{
    public class RedditCommentDao : BaseAuditDao<RedditComment, Guid>, IRedditCommentDao
    {
    }
}
