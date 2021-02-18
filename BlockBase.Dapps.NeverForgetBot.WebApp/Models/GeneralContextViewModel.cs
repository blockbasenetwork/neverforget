using BlockBase.Dapps.NeverForgetBot.Business.Pocos;
using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using System;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class GeneralContextViewModel
    {
        public Guid Id { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public int RequestTypeId { get; set; }

        public static GeneralContextViewModel FromData(GeneralContextPoco generalContext)
        {
            GeneralContextViewModel gcvm = new GeneralContextViewModel();

            gcvm.Id = generalContext.ContextId;
            gcvm.SourceType = generalContext.SourceType;
            gcvm.RequestTypeId = generalContext.RequestTypeId;

            gcvm.Content = generalContext.Content;
            gcvm.Author = generalContext.Author;
            gcvm.Date = generalContext.Date;
            if (gcvm.SourceType.Equals(SourceTypeEnum.Reddit) && gcvm.RequestTypeId == (int)RequestTypeEnum.Post)
            {
                gcvm.Title = generalContext.Title;
            }

            return gcvm;
        }
    }
}
