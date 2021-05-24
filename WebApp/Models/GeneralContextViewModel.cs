using BlockBase.Dapps.NeverForget.Common.Enums;
using BlockBase.Dapps.NeverForget.Data.Pocos;
using System;
using System.Web;

namespace BlockBase.Dapps.NeverForget.WebApp.Models
{
    public class GeneralContextViewModel
    {
        public Guid Id { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public string SubReddit { get; set; }

        public static GeneralContextViewModel FromData(GeneralContextPoco generalContext)
        {
            GeneralContextViewModel generalViewModel = new GeneralContextViewModel
            {
                Id = generalContext.Id,
                SourceType = generalContext.SourceType,
                Date = generalContext.Date,
                Author = generalContext.Author,
                Content = generalContext.Content,
                Link = generalContext.Link
            };

            if (generalContext.SubReddit != null)
                generalViewModel.SubReddit = generalContext.SubReddit;

            if (generalContext.Title != null)
                generalViewModel.Title = generalContext.Title;

            return generalViewModel;
        }
    }
}
