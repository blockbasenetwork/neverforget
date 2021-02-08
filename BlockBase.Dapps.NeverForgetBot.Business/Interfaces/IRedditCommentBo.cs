﻿using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.Interfaces
{
    public interface IRedditCommentBo : IBo<RedditComment>
    {
        Task<OperationResult> FromApiRedditCommentModel(RedditCommentModel modelArray, Guid id);
    }
}
