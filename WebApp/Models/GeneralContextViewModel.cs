using BlockBase.Dapps.NeverForget.Common.Enums;
using BlockBase.Dapps.NeverForget.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.WebApp.Models
{
    public class GeneralContextViewModel
    {
        public SourceTypeEnum SourceType { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; }

        public static GeneralContextViewModel FromData(GeneralContextPoco generalContext)
        {
            GeneralContextViewModel generalViewModel = new GeneralContextViewModel
            {
                SourceType = generalContext.SourceType,
                Date = generalContext.Date,
                Author = generalContext.Author,
                Content = generalContext.Content
            };

            if (generalContext.Title != null)
                generalViewModel.Title = generalContext.Title;

            return generalViewModel;
        }
    }
}
