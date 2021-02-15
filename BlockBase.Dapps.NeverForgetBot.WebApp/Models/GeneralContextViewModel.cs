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
        public string? Title { get; set; }
        public DateTime Date { get; set; }


        //public static GeneralContextViewModel FromData(GeneralContextPoco generalContext)
        //{
        //    GeneralContextViewModel gcvm = new GeneralContextViewModel();

        //    gcvm.Id = generalContext.ContextId;
        //    gcvm.SourceType = generalContext.SourceType;
        //    gcvm.PostType = generalContext.PostType;

        //    if (gcvm.SourceType.Equals(SourceTypeEnum.Reddit))
        //    {
        //        if (gcvm.PostType.Equals(PostTypeEnum.Comment))
        //        {
        //            gcvm.Content = generalContext.ContentComment;
        //            gcvm.Author = generalContext.AuthorComment;
        //            gcvm.Date = generalContext.DateComment;
        //        }
        //        else if (gcvm.PostType.Equals(PostTypeEnum.Submission))
        //        {
        //            gcvm.Content = generalContext.ContentSubmission;
        //            gcvm.Author = generalContext.AuthorSubmission;
        //            gcvm.Date = generalContext.DateSubmission;
        //        }
        //    }
        //    else if (gcvm.SourceType.Equals(SourceTypeEnum.Twitter))
        //    {
        //        if (gcvm.PostType.Equals(PostTypeEnum.Comment))
        //        {
        //            gcvm.Content = generalContext.ContentComment;
        //            gcvm.Author = generalContext.AuthorComment;
        //            gcvm.Date = generalContext.DateComment;
        //        }
        //        else if (gcvm.PostType.Equals(PostTypeEnum.Submission))
        //        {
        //            gcvm.Content = generalContext.ContentSubmission;
        //            gcvm.Author = generalContext.AuthorSubmission;
        //            gcvm.Date = generalContext.DateSubmission;
        //        }
        //    }
        //    return gcvm;
        //}
    }
}
