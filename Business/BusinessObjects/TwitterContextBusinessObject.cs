using BlockBase.Dapps.NeverForget.Business.BusinessModels;
using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.Common.Enums;
using BlockBase.Dapps.NeverForget.Data.Entities;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using BlockBase.Dapps.NeverForget.Services.API;
using BlockBase.Dapps.NeverForget.Services.API.Models;
using BlockBase.Dapps.NeverForget.Services.Resources;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.BusinessObjects
{
    public class TwitterContextBusinessObject : BaseAuditBusinessObject<TwitterContext>, ITwitterContextBusinessObject
    {
        private readonly ITwitterContextDataAccessObject _dataAccessObject;
        private readonly ITwitterCommentDataAccessObject _commentDataAccessObject;
        private readonly ITwitterSubmissionDataAccessObject _submissionDataAccessObject;
        private readonly ITwitterContextPocoDataAccessObject _pocoDataAccessObject;
        private readonly IRequestTypeDataAccessObject _requestTypeDataAccessObject;
        private readonly TwitterCollector _twitterCollector;

        string url = Web.Link + "twittercontexts/details/";
        string contextIdToPublish = string.Empty;

        public TwitterContextBusinessObject(ITwitterContextDataAccessObject dataAccessObject, ITwitterCommentDataAccessObject commentDataAccessObject, ITwitterSubmissionDataAccessObject submissionDataAccessObject, ITwitterContextPocoDataAccessObject pocoDataAccessObject, IRequestTypeDataAccessObject requestTypeDataAccessObject, TwitterCollector twitterCollector, ILogger<BaseBusinessObject> logger) : base(dataAccessObject, logger)
        {
            _dataAccessObject = dataAccessObject;
            _commentDataAccessObject = commentDataAccessObject;
            _submissionDataAccessObject = submissionDataAccessObject;
            _pocoDataAccessObject = pocoDataAccessObject;
            _requestTypeDataAccessObject = requestTypeDataAccessObject;
            _twitterCollector = twitterCollector;
        }

        public async Task<List<OperationResult>> FromApiTwitterModel(TweetModel[] modelArray)
        {
            List<OperationResult> opResults = new List<OperationResult>();

            var commentsToAdd = await _dataAccessObject.GetUniqueComments(modelArray);
            var requestTypes = await _requestTypeDataAccessObject.List();

            foreach (var model in commentsToAdd)
            {
                var result = await ExecuteOperation(async () =>
                {
                    #region Create Context
                    

                    var contextModel = new TwitterContext()
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        RequestTypeId = requestTypes.First(x => x.Type == CheckRequestType(model.Full_text)).Id
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
                                    await _dataAccessObject.InsertAsync(contextModel);
                                    await _commentDataAccessObject.InsertAsync(comment);
                                    await _submissionDataAccessObject.InsertAsync(parent);

                                    if (await _dataAccessObject.IsContextPresent(contextModel.Id) && await _dataAccessObject.IsSubmissionPresent(contextModel.Id))
                                    {
                                        await _twitterCollector.PublishUrl($"{url}{contextIdToPublish}", model.Id);
                                    }
                                }
                                else
                                {
                                    var comment = model.ToComment();
                                    comment.TwitterContextId = contextModel.Id;
                                    TwitterComment parent = tweetParent.ToComment();
                                    parent.TwitterContextId = contextModel.Id;
                                    await _dataAccessObject.InsertAsync(contextModel);
                                    await _commentDataAccessObject.InsertAsync(comment);
                                    await _commentDataAccessObject.InsertAsync(parent);

                                    if (await _dataAccessObject.IsContextPresent(contextModel.Id) && await _dataAccessObject.IsCommentPresent(contextModel.Id))
                                    {
                                        await _twitterCollector.PublishUrl($"{url}{contextIdToPublish}", model.Id);
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
                                await _dataAccessObject.InsertAsync(contextModel);
                                await _commentDataAccessObject.InsertAsync(comment);
                                await _twitterCollector.ReplyWithError(model.Id);
                            }
                        }
                        else
                        {
                            await _twitterCollector.ReplyWithUnable(model.Id);
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

                                await _dataAccessObject.InsertAsync(contextModel);
                                await _commentDataAccessObject.InsertAsync(comment);
                                await _submissionDataAccessObject.InsertAsync(submission);

                                if (await _dataAccessObject.IsContextPresent(contextModel.Id) && await _dataAccessObject.IsSubmissionPresent(contextModel.Id))
                                {
                                    await _twitterCollector.PublishUrl($"{url}{contextIdToPublish}", model.Id);
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
                                await _dataAccessObject.InsertAsync(contextModel);
                                await _commentDataAccessObject.InsertAsync(comment);
                                await _twitterCollector.ReplyWithError(model.Id);
                            }
                        }
                        else
                        {
                            await _twitterCollector.ReplyWithUnable(model.Id);
                        }
                    }
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
            else if (Regex.IsMatch(body, @"(@_neverforgetbot+ +comment)", RegexOptions.IgnoreCase))
            {
                return RequestTypeEnum.Comment;
            }
            else return RequestTypeEnum.Default;
        }
        #endregion



        public async Task<OperationResult<TwitterContextBusinessModel>> GetPocoAsync(Guid id)
        {
            return await ExecuteOperation(async () =>
            {
                var result = await _pocoDataAccessObject.GetTwitterContextById(id);

                var context = result.GroupBy(c => c.ContextId).FirstOrDefault();

                var detail = TwitterContextBusinessModel.From(id, context);

                return detail;
            });
        }

        public async Task<OperationResult<List<TwitterContextBusinessModel>>> GetAllPocoAsync()
        {
            return await ExecuteOperation(async () =>
            {
                var result = await _pocoDataAccessObject.GetAllTwitterContexts();

                var contexts = new List<TwitterContextBusinessModel>();

                var contextPocos = result.GroupBy(c => c.ContextId);

                foreach (var context in contextPocos)
                {
                    contexts.Add(TwitterContextBusinessModel.From(context.Key, context));
                }

                return contexts;
            });
        }
    }
}