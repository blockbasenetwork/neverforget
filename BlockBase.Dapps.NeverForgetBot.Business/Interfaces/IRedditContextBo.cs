using BlockBase.Dapps.NeverForgetBot.Business.BusinessModels;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.Interfaces
{
    public interface IRedditContextBo : IBo<RedditContextBusinessModel>
    {
        Task<OperationResult> FromApiRedditModel(RedditContextModel[] modelArray, RedditCommentModel[] commentArray);
    }
}
