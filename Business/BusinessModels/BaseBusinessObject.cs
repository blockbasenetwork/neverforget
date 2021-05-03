using BlockBase.Dapps.NeverForget.Business.BusinessModels.OperationResults;
using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.Data.Entities.Base;
using BlockBase.Dapps.NeverForget.Data.Interfaces;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.Business.BusinessModels
{
    public abstract class BaseBusinessObject
    {
        protected readonly IGenericDataAccessObject GenericDataAccessObject;
        private readonly ILogger<BaseBusinessObject> _logger;

        protected BaseBusinessObject(IGenericDataAccessObject genericDataAccessObject, ILogger<BaseBusinessObject> logger)
        {
            GenericDataAccessObject = genericDataAccessObject;
            _logger = logger;
        }

        protected async Task<OperationResult> ExecuteOperation(Func<Task> func)
        {
            try
            {
                await func.Invoke();
                return new OperationResult() { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new OperationResult() { Success = false, Exception = ex };
            }
        }

        protected async Task<OperationResult<TResult>> ExecuteOperation<TResult>(Func<Task<TResult>> func)
        {
            try
            {
                var result = await func.Invoke();
                return new OperationResult<TResult>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new OperationResult<TResult>(ex);
            }
        }

        protected async Task CheckIfExists<T>(Guid id, string alternativeName = "") where T : AuditEntity, IEntity
        {
            var record = await GenericDataAccessObject.Any<T>((t) => t.Id == id && !t.IsDeleted);
            if (!record)
            {
                if (alternativeName == "") alternativeName = typeof(T).Name;
                throw new Exception($"{alternativeName} with the id {id} was not found.");
            }
        }

    }

    public class BaseBusinessObject<T> : BaseBusinessObject, IBaseBusinessObject<T> where T : class
    {
        private readonly IBaseDataAccessObject<T> _baseDataAccessObject;

        public BaseBusinessObject(IBaseDataAccessObject<T> baseDataAccessObject, IGenericDataAccessObject genericDataAccessObject, ILogger<BaseBusinessObject> logger) : base(genericDataAccessObject, logger)
        {
            _baseDataAccessObject = baseDataAccessObject;
        }

        public async Task<OperationResult> InsertAsync(T record)
        {
            return await ExecuteOperation(async () => { await _baseDataAccessObject.InsertAsync(record); });
        }

        public async Task<OperationResult<IEnumerable<T>>> ListAsync()
        {
            return await ExecuteOperation(async () => await _baseDataAccessObject.List());
        }

        public async Task<OperationResult<T>> GetAsync(Guid id)
        {
            return await ExecuteOperation(async () => await _baseDataAccessObject.GetAsync(id));
        }

        public async Task<OperationResult> UpdateAsync(T record)
        {
            return await ExecuteOperation(async () =>
            {
                await _baseDataAccessObject.UpdateAsync(record);
            });
        }

        public async Task<OperationResult> DeleteAsync(T record)
        {
            return await ExecuteOperation(async () =>
            {
                await _baseDataAccessObject.DeleteAsync(record);
            });
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            return await ExecuteOperation(async () =>
            {
                await DeleteAsync(id);
            });
        }
    }

    public class BaseAuditBusinessObject<T> : BaseBusinessObject, IBaseBusinessObject<T> where T : AuditEntity, IEntity
    {
        private readonly IBaseAuditDataAccessObject<T> _baseAuditDataAccessObject;

        public BaseAuditBusinessObject(IBaseAuditDataAccessObject<T> baseAuditDataAccessObject, IGenericDataAccessObject genericDataAccessObject, ILogger<BaseBusinessObject> logger) : base(genericDataAccessObject, logger)
        {
            _baseAuditDataAccessObject = baseAuditDataAccessObject;
        }

        public async Task<OperationResult> InsertAsync(T record)
        {
            return await ExecuteOperation(async () => {
                record.CreatedAt = DateTime.UtcNow;
                await _baseAuditDataAccessObject.InsertAsync(record);
            });
        }

        public async Task<OperationResult<IEnumerable<T>>> ListAsync()
        {
            return await ExecuteOperation(async () => await _baseAuditDataAccessObject.List());
        }

        public async Task<OperationResult<T>> GetAsync(Guid id)
        {
            return await ExecuteOperation(async () => await _baseAuditDataAccessObject.GetAsync(id));
        }

        public async Task<OperationResult> UpdateAsync(T record)
        {
            return await ExecuteOperation(async () =>
            {
                await _baseAuditDataAccessObject.UpdateAsync(record);
            });
        }

        public async Task<OperationResult> DeleteAsync(T record)
        {
            return await ExecuteOperation(async () =>
            {
                record.IsDeleted = true;
                record.DeletedAt = DateTime.UtcNow;
                await _baseAuditDataAccessObject.UpdateAsync(record);
            });
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            return await ExecuteOperation(async () =>
            {
                var toDelete = await _baseAuditDataAccessObject.GetAsync(id);
                toDelete.IsDeleted = true;
                toDelete.DeletedAt = DateTime.UtcNow;
                await DeleteAsync(toDelete);
            });
        }
    }
}
