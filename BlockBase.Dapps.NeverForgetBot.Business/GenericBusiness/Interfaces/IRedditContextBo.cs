using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.GenericBusiness.Interfaces
{
    public interface IRedditContextBo : IBaseBo<RedditContext>
    {
        //Task<List<OperationResult>> FromApiRedditModel(RedditContextModel[] modelArray, RedditCommentModel[] commentArray);
    }
}
