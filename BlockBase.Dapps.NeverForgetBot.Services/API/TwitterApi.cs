using Tweetinvi;

namespace BlockBase.Dapps.NeverForgetBot.Services.API
{
    public class TwitterApi
    {
        public static TwitterClient Client { get; set; }

        private static readonly string _CONSUMER_KEY = "9qQx8bvfeJcPqdJl2DRgcsmcU";
        private static readonly string _CONSUMER_SECRET = "QD9iYlaRvHyz0795Ba2R8Mv02PV62F7zqzPubhn48VclIt78zv";
        private static readonly string _ACCESS_TOKEN = "1351921561155072002-aVv7lXhYNn8DKWMuTISpQzG1It1slU";
        private static readonly string _ACCESS_TOKEN_SECRET = "M6DPxMJyzgzT2oci5Z5c1T5Y70ZwtInYtwvTdp1GU9fiq";
        
        public static void AuthenticateClient()
        {
            //Client = new TwitterClient("CONSUMER_KEY", "CONSUMER_SECRET", "ACCESS_TOKEN", "ACCESS_TOKEN_SECRET");
            Client = new TwitterClient(_CONSUMER_KEY, _CONSUMER_SECRET, _ACCESS_TOKEN, _ACCESS_TOKEN_SECRET);
        }
    }
}