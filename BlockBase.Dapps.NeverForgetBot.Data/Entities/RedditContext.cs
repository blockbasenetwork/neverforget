﻿using BlockBase.BBLinq.Annotations;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table(Name = "RedditContexts")]
    public class RedditContext : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }


        public string CommentId { get; set; }

        public DateTime PostingDate { get; set; }

        public string Author { get; set; }

        public string SubReddit { get; set; }
    }
}