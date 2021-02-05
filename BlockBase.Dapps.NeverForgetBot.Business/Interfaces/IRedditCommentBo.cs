using BlockBase.Dapps.NeverForgetBot.Business.BusinessModels;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.Interfaces
{
    public interface IRedditCommentBo : IBo<RedditCommentBusinessModel>
    {
        Task<OperationResult> FromApiRedditCommentModel(RedditCommentModel[] modelArray, Guid id);
    }
}
