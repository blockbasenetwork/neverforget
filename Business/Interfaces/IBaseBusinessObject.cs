using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using BlockBase.Dapps.NeverForget.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.Interfaces
{
    public interface IBaseBusinessObject<TEntity> where TEntity : class, IEntity
    {
        Task<OperationResult> InsertAsync(TEntity entity);
        Task<OperationResult<TEntity>> GetAsync(Guid id);
        Task<OperationResult> DeleteAsync(TEntity entity);
        Task<OperationResult<List<TEntity>>> GetAllAsync();
    }
}
