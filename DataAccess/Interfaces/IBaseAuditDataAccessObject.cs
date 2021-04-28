using BlockBase.Dapps.NeverForget.Data.Entities.Base;
using BlockBase.Dapps.NeverForget.Data.Interfaces;

namespace BlockBase.Dapps.NeverForget.DataAccess.Interfaces
{
    public interface IBaseAuditDataAccessObject<TAuditEntity> : IBaseDataAccessObject<TAuditEntity> where TAuditEntity : AuditEntity, IEntity
    {
    }
}
