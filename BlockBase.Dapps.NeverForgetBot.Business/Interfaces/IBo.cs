using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Business.Interfaces
{
    public interface IBo<T>
    {
        Task<OperationResult<bool>> CreateAsync(T item);
        Task<OperationResult<T>> GetAsync(Guid id);
        Task<OperationResult> UpdateAsync(T item);
        Task<OperationResult<bool>> DeleteAsync(T item);
        Task<OperationResult<List<T>>> GetAllAsync();
    }
}
