using BlockBase.Dapps.NeverForget.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockBase.Dapps.NeverForget.Business.BusinessModels
{
    public class TwitterContextBusinessModel
    {
        public Guid Id { get; set; }
        public virtual ICollection<TwitterCommentBusinessModel> TwitterComments { get; set; }
        public virtual TwitterSubmissionBusinessModel TwitterSubmission { get; set; }

        public static TwitterContextBusinessModel From(IEnumerable<TwitterContextPoco> model, Guid contextId)
        {
            var context = new TwitterContextBusinessModel()
            {
                Id = contextId,
                TwitterComments = new List<TwitterCommentBusinessModel>()
            };

            foreach (var ctx in model)
            {
                var submission = TwitterSubmissionBusinessModel.From(ctx);

                if(submission != null)
                {
                    context.TwitterSubmission = submission;
                }

                var comment = TwitterCommentBusinessModel.From(ctx);

                if (comment != null)
                {
                    context.TwitterComments.Add(comment);
                }
            }

            return context;
        }
    }
}
