using BlockBase.BBLinq.Context;
using BlockBase.BBLinq.Sets;
using BlockBase.BBLinq.Settings;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Context
{
    public class NeverForgetBotDbContext : BbContext
    {
        //public NeverForgetBotDbContext(BbSettings settings) : base(settings)
        //{

        //}

        public NeverForgetBotDbContext() : base(new BbSettings() { DatabaseName = "testingRTB", NodeAddress = "http://40.121.160.216/nodedb1", PrivateKey = "5HzL18MQEMChpGsaEok364FdsQnjWHMS8yK76X7NvpPHLdZTsao", UserAccount = "sandbox" })
        {

        }

        public BbSet<RedditContext, Guid> RedditContext { get; set; }
        public BbSet<TwitterContext, Guid> TwitterContext { get; set; }
    }
}
