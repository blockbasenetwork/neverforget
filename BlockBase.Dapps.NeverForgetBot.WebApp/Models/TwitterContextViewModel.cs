using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class TwitterContextViewModel
    {
        public Guid Id { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public PostTypeEnum PostType { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }


        //public static TwitterContextViewModel FromData(TwitterContextPoco twitterContext)
        //{
        //    TwitterContextViewModel tcvm = new TwitterContextViewModel();

        //    tcvm.Id = twitterContext.ContextId;
        //    tcvm.PostType = twitterContext.PostType;

        //    if (tcvm.PostType.Equals(PostTypeEnum.Comment))
        //    {
        //        tcvm.Content = twitterContext.ContentComment;
        //        tcvm.Author = twitterContext.AuthorComment;
        //        tcvm.Date = twitterContext.CommentDate;
        //    }
        //    else if (tcvm.PostType.Equals(PostTypeEnum.Submission))
        //    {
        //        tcvm.Content = twitterContext.ContentSubmission;
        //        tcvm.Author = twitterContext.AuthorSubmission;
        //        tcvm.Date = twitterContext.SubmissionDate;
        //    }
        //    return tcvm;
        //}
    }
}
