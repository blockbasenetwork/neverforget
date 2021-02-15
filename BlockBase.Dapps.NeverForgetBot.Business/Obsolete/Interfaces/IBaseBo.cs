//using BlockBase.Dapps.NeverForgetBot.Business.OperationResults;
//using BlockBase.Dapps.NeverForgetBot.Data.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace BlockBase.Dapps.NeverForgetBot.Business.GenericBusiness.Interfaces
//{
//    public interface IBaseBo<TEntity> where TEntity : class, IEntity
//    {
//        Task<OperationResult> InsertAsync(TEntity entity);
//        Task<OperationResult<TEntity>> GetAsync(Guid id);
//        Task<OperationResult> DeleteAsync(TEntity entity);
//        Task<OperationResult<List<TEntity>>> GetAllAsync();
//    }
//}
