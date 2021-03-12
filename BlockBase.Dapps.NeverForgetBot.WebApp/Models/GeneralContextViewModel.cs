using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class GeneralContextViewModel
    {
        public SourceTypeEnum SourceType { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }

        public static GeneralContextViewModel FromData(GeneralContextPoco generalContext)
        {
            GeneralContextViewModel gcvm = new GeneralContextViewModel();

            gcvm.SourceType = generalContext.SourceType;
            gcvm.Content = generalContext.Content;
            gcvm.Author = generalContext.Author;
            gcvm.Date = generalContext.Date;

            if (generalContext.Title != null)
            {
                gcvm.Title = generalContext.Title;
            }

            return gcvm;
        }
    }
}
