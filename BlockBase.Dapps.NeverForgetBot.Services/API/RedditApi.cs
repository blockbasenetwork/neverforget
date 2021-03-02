using Reddit;
using Reddit.Controllers;
using Reddit.Inputs.Search;
using System.Collections.Generic;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class RedditApi
    {
        public static void AuthenticateClient()
        {
            var reddit = new RedditClient("z_FR2DzDnW3EEw", "YourBotUserRefreshToken");

            List<Post> posts = reddit.Subreddit("MySub").Search(new SearchGetSearchInput("Bernie Sanders"));  // Search r/MySub
            if (posts.Count == 0)
            {
                posts = reddit.Subreddit("all").Search(new SearchGetSearchInput("Bernie Sanders"));  // Search r/all
            }
        }

    }
}