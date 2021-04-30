using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.BusinessModels
{
    public class GenericBusinessObject : BaseBusinessObject, IGenericBusinessObject
    {
        public GenericBusinessObject(IGenericDataAccessObject genericDataAccessObject, ILogger<BaseBusinessObject> logger) : base(genericDataAccessObject, logger)
        {
        }

        public async Task<OperationResult> InsertAsync<T>(T record) where T : class
        {
            return await ExecuteOperation(async () => { await GenericDataAccessObject.InsertAsync(record); });
        }

        public async Task<OperationResult<IEnumerable<T>>> ListAsync<T>() where T : class
        {
            return await ExecuteOperation(async () => await GenericDataAccessObject.ListAsync<T>());
        }

        public async Task<OperationResult<T>> GetAsync<T>(Guid id) where T : class
        {
            return await ExecuteOperation(async () => await GenericDataAccessObject.GetAsync<T>(id));
        }

        public async Task<OperationResult> UpdateAsync<T>(T record) where T : class
        {
            return await ExecuteOperation(async () =>
            {
                await GenericDataAccessObject.UpdateAsync(record);
            });
        }

        public async Task<OperationResult> DeleteAsync<T>(T record) where T : class
        {
            return await ExecuteOperation(async () =>
            {
                await GenericDataAccessObject.DeleteAsync(record);
            });
        }

        public async Task<OperationResult> DeleteAsync<T>(Guid id) where T : class
        {
            return await ExecuteOperation(async () =>
            {
                var result = (await GetAsync<T>(id)).Result;
                if (result != null)
                {
                    await DeleteAsync(result);
                }
            });
        }
    }
}
