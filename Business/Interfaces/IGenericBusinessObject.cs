using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.Interfaces
{
    public interface IGenericBusinessObject
    {
        public Task<OperationResult> InsertAsync<T>(T record) where T : class;
        public Task<OperationResult<IEnumerable<T>>> ListAsync<T>() where T : class;
        public Task<OperationResult<T>> GetAsync<T>(Guid id) where T : class;
        public Task<OperationResult> UpdateAsync<T>(T record) where T : class;
        public Task<OperationResult> DeleteAsync<T>(T record) where T : class;
        public Task<OperationResult> DeleteAsync<T>(Guid id) where T : class;
    }
}
