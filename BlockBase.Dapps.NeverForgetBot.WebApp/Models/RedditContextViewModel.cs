﻿using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class RedditContextViewModel
    {
        public Guid Id { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public PostTypeEnum PostType { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Subreddit { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }


        //public static RedditContextViewModel FromData(RedditContextPoco redditContext)
        //{
        //    RedditContextViewModel rcvm = new RedditContextViewModel();

        //    rcvm.Id = redditContext.ContextId;
        //    rcvm.PostType = redditContext.PostType;

        //    if (rcvm.PostType.Equals(PostTypeEnum.Comment))
        //    {
        //        rcvm.Content = redditContext.ContentComment;
        //        rcvm.Author = redditContext.AuthorComment;
        //        rcvm.Date = redditContext.CommentDate;
        //        rcvm.Subreddit = redditContext.SubredditComment;
        //    }
        //    else if (rcvm.PostType.Equals(PostTypeEnum.Submission))
        //    {
        //        rcvm.Content = redditContext.ContentSubmission;
        //        rcvm.Author = redditContext.AuthorSubmission;
        //        rcvm.Date = redditContext.SubmissionDate;
        //        rcvm.Subreddit = redditContext.SubredditSubmission;
        //        rcvm.Title = redditContext.TitleSubmission;
        //    }
        //    return rcvm;
        //}
    }
}
