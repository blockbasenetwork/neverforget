using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Dal.Queries;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.BOs
{
    public class GeneralContextBo : IGeneralContextBo
    {
        private readonly IRedditContextPocoDao _redditDao;
        private readonly ITwitterContextPocoDao _twitterDao;
        private readonly IDbOperationExecutor _opExecutor;

        public GeneralContextBo(IRedditContextPocoDao redditDao, ITwitterContextPocoDao twitterDao, IDbOperationExecutor opExecutor)
        {
            _redditDao = redditDao;
            _twitterDao = twitterDao;
            _opExecutor = opExecutor;
        }

        public async Task<OperationResult<List<GeneralContextPoco>>> GetRecentCalls()
        {
            return await _opExecutor.ExecuteOperation<List<GeneralContextPoco>>(async () =>
            {
                var result = new List<GeneralContextPoco>();
                var recentReddits = await _redditDao.GetRecentRedditContexts();
                var recentTweets = await _twitterDao.GetRecentTwitterContexts();

                for (int i = 0; i < (recentReddits.Count); i++)
                {
                    if (recentReddits[i].Context.RequestTypeId == (int)RequestTypeEnum.Post)
                    {
                        var redditToGeneral = new GeneralContextPoco()
                        {
                            Author = recentReddits[i].Submission.Author,
                            Content = recentReddits[i].Submission.Content,
                            ContextId = recentReddits[i].Context.Id,
                            Date = recentReddits[i].Submission.SubmissionDate,
                            Link = recentReddits[i].Submission.Link,
                            MediaLink = recentReddits[i].Submission.MediaLink,
                            RequestTypeId = (int)RequestTypeEnum.Post,
                            SourceType = SourceTypeEnum.Reddit,
                            SubReddit = recentReddits[i].Submission.SubReddit,
                            Title = recentReddits[i].Submission.Title,
                        };
                        result.Add(redditToGeneral);
                    }
                    else if (recentReddits[i].Context.RequestTypeId == (int)RequestTypeEnum.Default || recentReddits[i].Context.RequestTypeId == (int)RequestTypeEnum.Comment)
                    {
                        recentReddits[i].Comments.OrderByDescending(c => c.CommentDate).ToList();
                        if (recentReddits[i].Comments.Count > 0 && recentReddits[i].Submission != null)
                        {
                            recentReddits[i].Comments.RemoveAt(0);
                        }

                        if (recentReddits[i].Comments.Count == 0)
                        {
                            var redditToGeneral = new GeneralContextPoco()
                            {
                                Author = recentReddits[i].Submission.Author,
                                Content = recentReddits[i].Submission.Content,
                                ContextId = recentReddits[i].Context.Id,
                                Date = recentReddits[i].Submission.SubmissionDate,
                                Link = recentReddits[i].Submission.Link,
                                MediaLink = recentReddits[i].Submission.MediaLink,
                                RequestTypeId = (int)RequestTypeEnum.Post,
                                SourceType = SourceTypeEnum.Reddit,
                                SubReddit = recentReddits[i].Submission.SubReddit,
                                Title = recentReddits[i].Submission.Title,
                            };

                            result.Add(redditToGeneral);
                        }
                        else
                        {
                            var redditToGeneral = new GeneralContextPoco()
                            {
                                Author = recentReddits[i].Comments[0].Author,
                                Content = recentReddits[i].Comments[0].Content,
                                ContextId = recentReddits[i].Context.Id,
                                Date = recentReddits[i].Comments[0].CommentDate,
                                Link = recentReddits[i].Comments[0].Link,
                                RequestTypeId = (int)RequestTypeEnum.Comment,
                                SourceType = SourceTypeEnum.Reddit,
                                SubReddit = recentReddits[i].Comments[0].SubReddit,
                            };

                            result.Add(redditToGeneral);
                        }
                    }
                }

                for (int i = 0; i < recentTweets.Count; i++)
                {
                    if (recentTweets[i].Context.RequestTypeId == (int)RequestTypeEnum.Post)
                    {
                        var tweetToGeneral = new GeneralContextPoco()
                        {
                            Author = recentTweets[i].Submission.Author,
                            Content = recentTweets[i].Submission.Content,
                            ContextId = recentTweets[i].Context.Id,
                            Date = recentTweets[i].Submission.SubmissionDate,
                            Link = recentTweets[i].Submission.Link,
                            MediaLink = recentTweets[i].Submission.MediaLink,
                            RequestTypeId = (int)RequestTypeEnum.Post,
                            SourceType = SourceTypeEnum.Twitter,
                        };
                        result.Add(tweetToGeneral);
                    }
                    else if (recentTweets[i].Context.RequestTypeId == (int)RequestTypeEnum.Default || recentTweets[i].Context.RequestTypeId == (int)RequestTypeEnum.Comment)
                    {
                        recentTweets[i].Comments.OrderByDescending(c => c.CommentDate).ToList();
                        recentTweets[i].Comments.RemoveAt(0);

                        if (recentTweets[i].Comments.Count == 0)
                        {
                            var tweetToGeneral = new GeneralContextPoco()
                            {
                                Author = recentTweets[i].Submission.Author,
                                Content = recentTweets[i].Submission.Content,
                                ContextId = recentTweets[i].Context.Id,
                                Date = recentTweets[i].Submission.SubmissionDate,
                                Link = recentTweets[i].Submission.Link,
                                MediaLink = recentTweets[i].Submission.MediaLink,
                                RequestTypeId = (int)RequestTypeEnum.Post,
                                SourceType = SourceTypeEnum.Twitter,
                            };

                            result.Add(tweetToGeneral);
                        }
                        else
                        {
                            var tweetToGeneral = new GeneralContextPoco()
                            {
                                Author = recentTweets[i].Comments[0].Author,
                                Content = recentTweets[i].Comments[0].Content,
                                ContextId = recentTweets[i].Context.Id,
                                Date = recentTweets[i].Comments[0].CommentDate,
                                Link = recentTweets[i].Comments[0].Link,
                                MediaLink = recentTweets[i].Comments[0].MediaLink,
                                RequestTypeId = (int)RequestTypeEnum.Comment,
                                SourceType = SourceTypeEnum.Twitter,
                            };

                            result.Add(tweetToGeneral);
                        }
                    }
                }

                result = result.OrderByDescending(c => c.Date).Take(10).ToList();

                return result;
            });
        }
    }
}
