﻿using BlockBase.BBLinq.DataAnnotations;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table("RedditComments")]
    public class RedditComment : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string CommentId { get; set; }
        public string ParentId { get; set; }
        public string ParentSubmissionId { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public string Author { get; set; }
        public string SubReddit { get; set; }
        public string Link { get; set; }

        [ForeignKey(typeof(RedditContext))]
        public Guid RedditContextId { get; set; }
    }
}
