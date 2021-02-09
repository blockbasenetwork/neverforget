using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.NeverForgetBot.Services.API.Models
{
    public class TweetModel
    {
        public string Id { get; set; }
        public string Full_text { get; set; }
        public TweetAuthor User { get; set; }
        public DateTime Created_at { get; set; }
        public string? In_reply_to_status_id_str { get; set; }
        public string? In_reply_to_user_id_str { get; set; }
        public string? In_reply_to_screen_name { get; set; }
    }

    public class TweetAuthor
    {
        public string Screen_name { get; set; }
        public string Id_str { get; set; }
    }
}
