using BlockBase.Dapps.NeverForgetBot.Common.Enums;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Models
{
    public class RedditContextViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("UserRole")]
        [Display(Name = "RequestType")]
        public int RequestTypeId { get; set; }
        public RequestTypeEnum RequestType { get; set; }

        public static RedditContextViewModel FromData(RedditContext context)
        {
            return new RedditContextViewModel()
            {
                Id = context.Id,
                RequestTypeId = context.RequestTypeId,
                RequestType = (RequestTypeEnum)context.RequestTypeId
            };
        }
    }
}
