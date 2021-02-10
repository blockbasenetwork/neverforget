using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;

namespace BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces
{
    public interface IBaseAuditDao<TAuditEntity, TKey> : IBaseDao<TAuditEntity, TKey> where TAuditEntity : AuditEntity, IEntity
    {
    }
}
