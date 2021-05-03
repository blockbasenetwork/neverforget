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
            DatabaseName = "NeverForgetTesting",
            Host = "http://40.121.160.216/nodedb1",
            Password = "5HzL18MQEMChpGsaEok364FdsQnjWHMS8yK76X7NvpPHLdZTsao",
            UserAccount = "sandbox"
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
