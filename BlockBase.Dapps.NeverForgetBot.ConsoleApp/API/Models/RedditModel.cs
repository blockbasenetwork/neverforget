using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BlockBase.Dapps.NeverForgetBot.ConsoleApp.API.Models
{
    public class RedditResultModel
    {
        public RedditModel[] Data { get; set; }
    }
    public class RedditModel
    {
        public string Author { get; set; }
        public string Body { get; set; }
        public int Created_Utc { get; set; }
        public string Id { get; set; }
        public string Subreddit { get; set; }

    }

    
}
