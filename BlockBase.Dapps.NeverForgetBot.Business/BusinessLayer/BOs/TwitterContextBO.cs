using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Dal.Queries;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using BlockBase.Dapps.NeverForgetBot.Services.API;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.BOs
{
    public class TwitterContextBo : ITwitterContextBo
    {
        private readonly ITwitterContextDao _dao;
        private readonly IDbOperationExecutor _opExecutor;
        private readonly ITwitterCommentDao _commentDao;
        private readonly ITwitterSubmissionDao _submissionDao;
        private readonly ITwitterContextPocoDao _pocoDao;
        private readonly TwitterCollector _twitterCollector;

        string url = "http://web-app-neverforget-blockbase.azurewebsites.net/twittercontexts/details/";
        string contextIdToPublish = string.Empty;

        public TwitterContextBo(ITwitterContextDao dao, IDbOperationExecutor opExecutor, ITwitterCommentDao commentDao, ITwitterSubmissionDao submissionDao, ITwitterContextPocoDao pocoDao, TwitterCollector twitterCollector)
        {
            _dao = dao;
            _opExecutor = opExecutor;
            _commentDao = commentDao;
            _submissionDao = submissionDao;
            _pocoDao = pocoDao;
            _twitterCollector = twitterCollector;
        }

        public async Task<List<OperationResult>> FromApiTwitterModel(TweetModel[] modelArray)
        {
            List<OperationResult> opResults = new List<OperationResult>();

            var commentsToAdd = await _dao.GetUniqueComments(modelArray);

            foreach (var model in commentsToAdd)
            {
                var result = await _opExecutor.ExecuteOperation(async () =>
                {
                    #region Create Context
                    var contextModel = new TwitterContext()
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        RequestTypeId = (int)CheckRequestType(model.Full_text)
                    };
                    var requestType = CheckRequestType(model.Full_text);
                    contextIdToPublish = contextModel.Id.ToString();
                    #endregion

                    #region Request Type conditions
                    if (requestType == RequestTypeEnum.Comment || requestType == RequestTypeEnum.Default)
                    {
                        if (model.In_reply_to_status_id_str != null)
                        {
                            var tweetParent = await _twitterCollector.GetTweet(model.In_reply_to_status_id_str);
                            if (tweetParent.Id != null)
                            {
                                if (tweetParent.In_reply_to_status_id_str == null)
                                {
                                    var comment = model.ToComment();
                                    comment.TwitterContextId = contextModel.Id;
                                    TwitterSubmission parent = tweetParent.ToSubmission();
                                    parent.TwitterContextId = contextModel.Id;
                                    await _dao.InsertAsync(contextModel);
                                    await _commentDao.InsertAsync(comment);
                                    await _submissionDao.InsertAsync(parent);

                                    if (await _dao.IsContextPresent(contextModel.Id) && await _dao.IsSubmissionPresent(contextModel.Id))
                                    {
                                        //await _twitterCollector.PublishUrl($"{url}{contextIdToPublish}", model.Id);
                                    }
                                }
                                else
                                {
                                    var comment = model.ToComment();
                                    comment.TwitterContextId = contextModel.Id;
                                    TwitterComment parent = tweetParent.ToComment();
                                    parent.TwitterContextId = contextModel.Id;
                                    await _dao.InsertAsync(contextModel);
                                    await _commentDao.InsertAsync(comment);
                                    await _commentDao.InsertAsync(parent);

                                    if (await _dao.IsContextPresent(contextModel.Id) && await _dao.IsCommentPresent(contextModel.Id))
                                    {
                                        //await _twitterCollector.PublishUrl($"{url}{contextIdToPublish}", model.Id);
                                    }
                                }
                            }
                            else
                            {
                                var comment = model.ToComment();
                                comment.TwitterContextId = contextModel.Id;
                                comment.CommentId = model.Id;
                                comment.Content = "[Deleted]";
                                comment.Author = "[N/A]";
                                comment.CommentDate = DateTime.UtcNow;
                                comment.Link = "[N/A]";
                                comment.IsDeleted = true;
                                comment.DeletedAt = DateTime.UtcNow;
                                contextModel.IsDeleted = true;
                                contextModel.DeletedAt = DateTime.UtcNow;
                                await _dao.InsertAsync(contextModel);
                                await _commentDao.InsertAsync(comment);
                                //await _twitterCollector.ReplyWithError(model.Id);
                            }
                        }
                        else
                        {
                            //await _twitterCollector.ReplyWithUnable(model.Id);
                        }
                    }

                    else if (requestType == RequestTypeEnum.Post)
                    {
                        if (model.In_reply_to_status_id_str != null)
                        {
                            var submission = await GetSubmissionFrom(model, contextModel.Id);

                            if (submission != null)
                            {
                                TwitterComment comment = model.ToComment();
                                comment.TwitterContextId = contextModel.Id;

                                await _dao.InsertAsync(contextModel);
                                await _commentDao.InsertAsync(comment);
                                await _submissionDao.InsertAsync(submission);

                                if (await _dao.IsContextPresent(contextModel.Id) && await _dao.IsSubmissionPresent(contextModel.Id))
                                {
                                    //await _twitterCollector.PublishUrl($"{url}{contextIdToPublish}", model.Id);
                                }
                            }
                            else
                            {
                                var comment = model.ToComment();
                                comment.TwitterContextId = contextModel.Id;
                                comment.CommentId = model.Id;
                                comment.Content = "[Deleted]";
                                comment.Author = "[N/A]";
                                comment.CommentDate = DateTime.UtcNow;
                                comment.Link = "[N/A]";
                                comment.IsDeleted = true;
                                comment.DeletedAt = DateTime.UtcNow;
                                contextModel.IsDeleted = true;
                                contextModel.DeletedAt = DateTime.UtcNow;
                                await _dao.InsertAsync(contextModel);
                                await _commentDao.InsertAsync(comment);
                                //await _twitterCollector.ReplyWithError(model.Id);
                            }
                        }
                        else
                        {
                            //await _twitterCollector.ReplyWithUnable(model.Id);
                        }
                    }
                    #endregion

                    #region To be implemented
                    /*else if (requestType == RequestTypeEnum.Thread)
                    {
                        //await _commentBo.FromApiTwitterCommentModel(model, contextModel.Id);
                        TwitterComment comment = model.ToComment();
                        comment.TwitterContextId = contextModel.Id;
                        await _commentDao.InsertAsync(comment);

                        await GetAndInsertAllParentComment(model, contextModel.Id);
                    }*/
                    #endregion
                });

                opResults.Add(result);
            }
            return opResults;
        }

        #region Process Data

        private async Task<TwitterSubmission> GetSubmissionFrom(TweetModel tweet, Guid id)
        {
            if (tweet.In_reply_to_status_id_str != null)
            {
                do
                {
                    tweet = await _twitterCollector.GetTweet(tweet.In_reply_to_status_id_str);
                } while (tweet.In_reply_to_status_id_str != null);

            }

            if (tweet.Id == null)
            {
                return null;
            }

            TwitterSubmission submission = tweet.ToSubmission();
            submission.TwitterContextId = id;

            return submission;
        }

        private RequestTypeEnum CheckRequestType(string body)
        {
            if (Regex.IsMatch(body, @"(@_neverforgetbot+ +post)", RegexOptions.IgnoreCase))
            {
                return RequestTypeEnum.Post;
            }
            //else if (Regex.IsMatch(body, @"(@_neverforgetbot+ +thread)", RegexOptions.IgnoreCase))
            //{
            //    return RequestTypeEnum.Thread;
            //}
            else if (Regex.IsMatch(body, @"(@_neverforgetbot+ +comment)", RegexOptions.IgnoreCase))
            {
                return RequestTypeEnum.Comment;
            }
            else return RequestTypeEnum.Default;
        }

        #endregion

        #region Create
        public async Task<OperationResult> InsertAsync(TwitterContext twitterContext)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterContext.CreatedAt = DateTime.UtcNow;
                await _dao.InsertAsync(twitterContext);
            });
        }
        #endregion

        #region Read
        public async Task<OperationResult<TwitterContext>> GetAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<TwitterContext>(async () =>
            {
                var result = await _dao.GetAsync(id);
                return result;
            });
        }


        public async Task<OperationResult<TwitterContextPoco>> GetPocoAsync(Guid id)
        {
            return await _opExecutor.ExecuteOperation<TwitterContextPoco>(async () =>
            {
                var result = await _pocoDao.GetTwitterContextById(id);
                return result;
            });
        }
        #endregion

        #region Delete
        public async Task<OperationResult> DeleteAsync(TwitterContext twitterContext)
        {
            return await _opExecutor.ExecuteOperation(async () =>
            {
                twitterContext.IsDeleted = true;
                twitterContext.DeletedAt = DateTime.UtcNow;
                var contextDelete = await _dao.GetAsync(twitterContext.Id);
                await _dao.DeleteAsync(contextDelete);
            });
        }
        #endregion

        #region List
        public async Task<OperationResult<List<TwitterContext>>> GetAllAsync()
        {
            return await _opExecutor.ExecuteOperation<List<TwitterContext>>(async () =>
            {
                var result = await _dao.GetAllAsync();
                return result.Select(context => context).ToList();
            });
        }


        public async Task<OperationResult<List<TwitterContextPoco>>> GetAllPocoAsync()
        {
            return await _opExecutor.ExecuteOperation<List<TwitterContextPoco>>(async () =>
            {
                var result = await _pocoDao.GetAllTwitterContexts();
                return result.Select(context => context).ToList();
            });
        }
        #endregion
    }
}
