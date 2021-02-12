using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class GeneralContextViewModel
    {
        public Guid Id { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public PostTypeEnum PostType { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string? MediaLink { get; set; }
        public string Link { get; set; }
        public string? SubReddit { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }

        public GeneralContextViewModel()
        {
            this.GeneralContextPocos = new List<GeneralContextPoco>();
        }

        public List<GeneralContextPoco> GeneralContextPocos { get; set; }

        public static GeneralContextViewModel FromData(GeneralContextPoco generalContext)
        {
            GeneralContextViewModel poco = new GeneralContextViewModel();

            poco.Id = generalContext.ContextId;
            poco.SourceType = generalContext.SourceType;
            poco.PostType = generalContext.PostType;

            if (poco.SourceType.Equals(SourceTypeEnum.Reddit))
            {
                if (poco.PostType.Equals(PostTypeEnum.Comment))
                {
                    poco.Content = generalContext.ContentComment;
                    poco.Author = generalContext.AuthorComment;
                    poco.Link = generalContext.LinkComment;
                    poco.SubReddit = generalContext.SubRedditComment;
                    poco.Date = generalContext.DateComment;
                }
                else if (poco.PostType.Equals(PostTypeEnum.Submission))
                {
                    poco.Content = generalContext.ContentSubmission;
                    poco.Content = generalContext.AuthorSubmission;
                    poco.Link = generalContext.LinkSubmission;
                    poco.MediaLink = generalContext.MediaLinkSubmission;
                    poco.SubReddit = generalContext.SubRedditSubmission;
                    poco.Date = generalContext.DateSubmission;
                }
            }
            else if (poco.SourceType.Equals(SourceTypeEnum.Twitter))
            {
                if (poco.PostType.Equals(PostTypeEnum.Comment))
                {
                    poco.Content = generalContext.ContentComment;
                    poco.Author = generalContext.AuthorComment;
                    poco.MediaLink = generalContext.MediaLinkComment;
                    poco.Link = generalContext.LinkComment;
                    poco.Date = generalContext.DateComment;
                }
                else if (poco.PostType.Equals(PostTypeEnum.Submission))
                {
                    poco.Content = generalContext.ContentSubmission;
                    poco.Content = generalContext.AuthorSubmission;
                    poco.Link = generalContext.LinkSubmission;
                    poco.MediaLink = generalContext.MediaLinkSubmission;
                    poco.Date = generalContext.DateSubmission;
                }
            }

            return poco;
        }
    }
}
