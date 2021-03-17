using BlockBase.Dapps.NeverForgetBot.Data.Entities.Base;
using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;

namespace BlockBase.Dapps.NeverForgetBot.Dal.GenericDataAccess.Interfaces
{
    public interface IBaseAuditDao<TAuditEntity> : IBaseDao<TAuditEntity> where TAuditEntity : AuditEntity, IEntity
    {
    }
}
