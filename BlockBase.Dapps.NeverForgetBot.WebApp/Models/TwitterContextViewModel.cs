using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class TwitterContextViewModel
    {
        public TwitterContextViewModel()
        {
            this.TwitterContextPocos = new List<TwitterContextPoco>();
        }

        public List<TwitterContextPoco> TwitterContextPocos { get; set; }
    }
}
