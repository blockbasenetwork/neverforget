﻿using BlockBase.BBLinq.Annotations;
using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Entities
{
    [Table(Name = "TwitterSubmissions")]
    public class TwitterSubmission : AuditEntity, IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string SubmissionId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string? MediaLink { get; set; }
        public string Link { get; set; }
        public DateTime SubmissionDate { get; set; }

        [ForeignKey(Name = "TwitterContexts")]
        public Guid TwitterContextId { get; set; }
    }
}
