using BlockBase.Dapps.NeverForgetBot.Business.Obsolete.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using BlockBase.Dapps.NeverForgetBot.Services.API;
using BlockBase.Dapps.NeverForgetBot.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.Obsolete.BOs
{
    public class TwitterContextBo : ITwitterContextBo
    {
        private readonly ITwitterContextDao _dao;
        private readonly IDbOperationExecutor _opExecutor;
        private readonly ITwitterCommentDao _commentDao;
        private readonly ITwitterSubmissionDao _submissionDao;
        private readonly TwitterCollector _twitterCollector;

        public TwitterContextBo(ITwitterContextDao dao, IDbOperationExecutor opExecutor, ITwitterCommentDao commentDao, ITwitterSubmissionDao submissionDao, TwitterCollector twitterCollector)
        {
            _dao = dao;
            _opExecutor = opExecutor;
            _commentDao = commentDao;
            _submissionDao = submissionDao;
            _twitterCollector = twitterCollector;
        }

        public async Task<List<OperationResult>> FromApiTwitterModel(TweetModel[] modelArray)
        {
            List<OperationResult> opResults = new List<OperationResult>();

            foreach (var model in modelArray)
            {
                var result = await _opExecutor.ExecuteOperation(async () =>
                {
                    if (!_commentDao.GetAllAsync().Result.Any(c => c.CommentId == model.Id))
                    {
                        var contextModel = new TwitterContext()
                        {
                            Id = Guid.NewGuid(),
                            CreatedAt = DateTime.UtcNow
                        };
                        await _dao.InsertAsync(contextModel);

                        var requestType = CheckRequestType(model.Full_text);
                        if (requestType == RequestTypeEnum.Comment || requestType == RequestTypeEnum.Default)
                        {
                            TwitterComment comment = model.ToComment();
                            comment.TwitterContextId = contextModel.Id;
                            await _commentDao.InsertAsync(comment);

                            if (model.In_reply_to_status_id_str != null)
                            {
                                var tweetParent = await _twitterCollector.GetTweet(model.In_reply_to_status_id_str);

                                TwitterComment parent = tweetParent.ToComment();
                                parent.TwitterContextId = contextModel.Id;
                                await _commentDao.InsertAsync(parent);
                            }
                            else
                            {
                                var tweetParent = await _twitterCollector.GetTweet(model.In_reply_to_status_id_str);

                                TwitterSubmission submission = tweetParent.ToSubmission();
                                submission.TwitterContextId = contextModel.Id;
                                await _submissionDao.InsertAsync(submission);
                            }
                        }
                        else if (requestType == RequestTypeEnum.Post)
                        {
                            TwitterComment comment = model.ToComment();
                            comment.TwitterContextId = contextModel.Id;
                            await _commentDao.InsertAsync(comment);

                            TwitterSubmission submission = await GetSubmissionFrom(model, contextModel.Id);
                            await _submissionDao.InsertAsync(submission);
                        }
                        /*else if (requestType == RequestTypeEnum.Thread)
                        {
                            //await _commentBo.FromApiTwitterCommentModel(model, contextModel.Id);
                            TwitterComment comment = model.ToComment();
                            comment.TwitterContextId = contextModel.Id;
                            await _commentDao.InsertAsync(comment);

                            await GetAndInsertAllParentComment(model, contextModel.Id);
                        }*/
                    }
                });
                opResults.Add(result);
            }

            return opResults;
        }


        private async Task<TwitterSubmission> GetSubmissionFrom(TweetModel tweet, Guid id)
        {
            if (tweet.In_reply_to_status_id_str != null)
            {
                do
                {
                    tweet = await _twitterCollector.GetTweet(tweet.In_reply_to_status_id_str);
                } while (tweet.In_reply_to_status_id_str != null);
            }

            TwitterSubmission submission = tweet.ToSubmission();
            submission.TwitterContextId = id;

            return submission;
        }

        /*public async Task<OperationResult> GetAndInsertAllParentComment(TweetModel tweet, Guid id)
        {
            if (tweet.In_reply_to_status_id_str != null)
            {
                List<TweetModel> thread = await GetThreadFromTweet(tweet.In_reply_to_status_id_str);

                foreach (var t in thread)
                {
                    if (t.In_reply_to_status_id_str != null)
                    {
                        await _commentBo.FromApiTwitterCommentModel(t, id);
                    }
                    else
                    {
                        await GetAndInsertSubmission(t, id);
                    }
                }
            }

            return new OperationResult() { Success = true };
        }*/


        private RequestTypeEnum CheckRequestType(string body)
        {
            if (Regex.IsMatch(body, @"(@_neverforgetbot+ +post)", RegexOptions.IgnoreCase))
            {
                return RequestTypeEnum.Post;
            }
            else if (Regex.IsMatch(body, @"(@_neverforgetbot+ +thread)", RegexOptions.IgnoreCase))
            {
                return RequestTypeEnum.Thread;
            }
            else if (Regex.IsMatch(body, @"(@_neverforgetbot+ +comment)", RegexOptions.IgnoreCase))
            {
                return RequestTypeEnum.Comment;
            }
            else return RequestTypeEnum.Default;
        }



        /*
        public async Task<OperationResult> GetAndInsertParentComment(TweetModel tweet, Guid id)
        {
            if (tweet.In_reply_to_status_id_str != null)
            {
                var tweetInfo = await _twitterCollector.GetTweet(tweet.In_reply_to_status_id_str);
                await _commentBo.FromApiTwitterCommentModel(tweetInfo, id);
            }
            else
            {
                var tweetInfo = await _twitterCollector.GetTweet(tweet.In_reply_to_status_id_str);
                await _submissionBo.FromApiTwitterSubmissionModel(tweetInfo, id);
            }

            return new OperationResult() { Success = true };
        }

        public async Task<OperationResult> GetAndInsertAllParentComment(TweetModel tweet, Guid id)
        {
            if (tweet.In_reply_to_status_id_str != null)
            {
                List<TweetModel> thread = await GetThreadFromTweet(tweet.In_reply_to_status_id_str);

                foreach (var t in thread)
                {
                    if (t.In_reply_to_status_id_str != null)
                    {
                        await _commentBo.FromApiTwitterCommentModel(t, id);
                    }
                    else
                    {
                        await GetAndInsertSubmission(t, id);
                    }
                }
            }

            return new OperationResult() { Success = true };
        }

        public async Task<OperationResult> GetAndInsertSubmission(TweetModel tweet, Guid id)
        {
            if (tweet.In_reply_to_status_id_str != null)
            {
                var submission = await _twitterCollector.GetSubmissionFromTweet(tweet.In_reply_to_status_id_str);
                await _submissionBo.FromApiTwitterSubmissionModel(submission, id);
            }
            else
            {
                await _submissionBo.FromApiTwitterSubmissionModel(tweet, id);
            }


            return new OperationResult() { Success = true };
        }

        #region HELP
        public async Task<List<TweetModel>> GetThreadFromTweet(string id)
        {
            List<TweetModel> result = new List<TweetModel>();

            var request = await _twitterCollector.GetTweet(id);

            result.Add(request);

            if (request.In_reply_to_status_id_str != null)
            {
                do
                {
                    var parent = await _twitterCollector.GetTweet(result.Last().In_reply_to_status_id_str);

                    result.Add(parent);
                } while (result.Last().In_reply_to_status_id_str != null);
            }

            return result;
        }
        public TwitterComment ApiToTwitterComment(TweetModel tweet)
        {
            TwitterComment comment = new TwitterComment();

            comment.Id = Guid.NewGuid();
            comment.TwitterContextId = Guid.Empty;
            comment.CommentId = tweet.Id;
            comment.ReplyToId = tweet.In_reply_to_status_id_str;
            comment.Content = tweet.Full_text;
            comment.Author = tweet.User.Screen_name;
            comment.CommentDate = tweet.Created_at;
            comment.CreatedAt = DateTime.UtcNow;

            return comment;
        }
        public TwitterSubmission ApiToTwitterSubmission(TweetModel tweet)
        {
            TwitterSubmission submission = new TwitterSubmission();

            submission.Id = Guid.NewGuid();
            submission.TwitterContextId = Guid.Empty;
            submission.SubmissionId = tweet.Id;
            submission.Content = tweet.Full_text;
            submission.Author = tweet.User.Screen_name;
            submission.SubmissionDate = tweet.Created_at;
            submission.CreatedAt = DateTime.UtcNow;

            return submission;
        }
        private RequestTypeEnum CheckRequestType(string body)
        {
            if (body.ToLower().Contains("@_neverforgetbot post"))
            {
                return RequestTypeEnum.Post;
            }
            else if (body.ToLower().Contains("@_neverforgetbot thread"))
            {
                return RequestTypeEnum.Thread;
            }
            else return RequestTypeEnum.Comment;
        }
        #endregion
        */



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
        #endregion
    }
}
