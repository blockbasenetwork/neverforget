using BlockBase.Dapps.NeverForget.Data.Entities.Base;
using BlockBase.Dapps.NeverForget.Data.Interfaces;

namespace BlockBase.Dapps.NeverForget.DataAccess.Interfaces
{
    public interface IBaseAuditDao<TAuditEntity> : IBaseDao<TAuditEntity> where TAuditEntity : AuditEntity, IEntity
    {
    }
}
