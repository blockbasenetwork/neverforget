using BlockBase.BBLinq.Annotations;
using System;

namespace BlockBase.Dapps.NeverForgetBot.Data.Interfaces
{
    public interface IEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
    }
}
