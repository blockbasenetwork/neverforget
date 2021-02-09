using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.Interfaces
{
    public interface ITwitterCommentBo : IBo<TwitterComment>
    {
        Task<OperationResult> FromApiTwitterCommentModel(TweetModel modelArray, Guid id);
    }
}
