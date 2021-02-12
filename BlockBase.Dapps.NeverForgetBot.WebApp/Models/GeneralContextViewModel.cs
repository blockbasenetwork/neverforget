using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class GeneralContextViewModel
    {
        public GeneralContextViewModel()
        {
            this.DetailsDataPocos = new List<GeneralContextPoco>();
        }

        public List<GeneralContextPoco> DetailsDataPocos { get; set; }
    }
}
