using BlockBase.Dapps.NeverForget.Data.Pocos;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForget.Business.BusinessModels
{
    public class RedditContextBusinessModel
    {
        public Guid Id { get; set; }
        public virtual ICollection<RedditCommentBusinessModel> RedditComments { get; set; }
        public virtual RedditSubmissionBusinessModel RedditSubmission { get; set; }

        public static RedditContextBusinessModel From(Guid contextId, IEnumerable<RedditContextPoco> model)
        {
            var context = new RedditContextBusinessModel()
            {
                Id = contextId,
                RedditComments = new List<RedditCommentBusinessModel>()
            };

            foreach (var ctx in model)
            {
                var submission = RedditSubmissionBusinessModel.From(ctx);

                if (submission != null)
                {
                    context.RedditSubmission = submission;
                }

                var comment = RedditCommentBusinessModel.From(ctx);

                if (comment != null)
                {
                    context.RedditComments.Add(comment);
                }
            }

            return context;
        }
    }
}
