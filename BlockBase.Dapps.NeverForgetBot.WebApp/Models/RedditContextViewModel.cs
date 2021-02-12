using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class RedditContextViewModel
    {
        public RedditContextViewModel()
        {
            this.RedditContextPocos = new List<RedditContextPoco>();
        }

        public List<RedditContextPoco> RedditContextPocos { get; set; }
    }
}
