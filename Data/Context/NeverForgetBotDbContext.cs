using BlockBase.BBLinq.Contexts;
using BlockBase.BBLinq.Sets;
using BlockBase.BBLinq.Settings;
using BlockBase.Dapps.NeverForget.Data.Entities;

namespace BlockBase.Dapps.NeverForget.Data.Context
{
    public class NeverForgetBotDbContext : BlockBaseContext
    {
        public NeverForgetBotDbContext() : base(new BlockBaseSettings()
        {
            DatabaseName = "NeverForgetBot",
            Host = "https://20.51.248.230:5000",
            Password = "5JEJ5TtiEtyoWAUiBF2iTiMcji9yZ6nBqTJy79mE54a7MHaR1WE",
            UserAccount = "bbtestacc124"
        })
        { }

        public BlockBaseSet<RedditContext> RedditContext { get; set; }
        public BlockBaseSet<RedditComment> RedditComment { get; set; }
        public BlockBaseSet<RedditSubmission> RedditSubmission { get; set; }
        public BlockBaseSet<TwitterContext> TwitterContext { get; set; }
        public BlockBaseSet<TwitterComment> TwitterComment { get; set; }
        public BlockBaseSet<TwitterSubmission> TwitterSubmission { get; set; }
        public BlockBaseSet<RequestType> RequestType { get; set; }
    }
}
