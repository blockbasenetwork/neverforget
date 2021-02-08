using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.Interfaces
{
    public interface IRedditSubmissionBo : IBo<RedditSubmission>
    {
        Task<OperationResult> FromApiRedditSubmissionModel(RedditSubmissionModel modelArray, Guid id);
    }
}
