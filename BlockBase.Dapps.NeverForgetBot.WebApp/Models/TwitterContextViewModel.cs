using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class TwitterContextViewModel
    {
        public TwitterContextViewModel()
        {
            this.DetailsDataPocos = new List<TwitterContextPoco>();
        }

        public List<TwitterContextPoco> DetailsDataPocos { get; set; }
    }
}
