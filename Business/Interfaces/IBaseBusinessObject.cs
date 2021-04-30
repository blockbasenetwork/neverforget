using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.Interfaces
{
    public interface IBaseBusinessObject<T> where T : class
    {
        public Task<OperationResult> InsertAsync(T record);
        public Task<OperationResult<IEnumerable<T>>> ListAsync();
        public Task<OperationResult<T>> GetAsync(Guid id);
        public Task<OperationResult> UpdateAsync(T record);
        public Task<OperationResult> DeleteAsync(T record);
        public Task<OperationResult> DeleteAsync(Guid id);
    }
}
