using BlockBase.Dapps.NeverForgetBot.Dal.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Context;
using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.DAOs
{
    public class RequestTypeDao : IRequestTypeDao
    {
        #region Create
        public async Task InsertAsync(RequestType entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.RequestType.Insert(entity);
            }
        }
        #endregion

        #region Read
        public async Task<RequestType> GetAsync(int id)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.RequestType.Get(id);
                return result.Result;
            }
        }
        #endregion

        #region Update
        public async Task UpdateAsync(RequestType entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.RequestType.Update(entity);
            }
        }
        #endregion

        #region Delete
        public async Task HardDeleteAsync(RequestType entity)
        {
            using (var context = new NeverForgetBotDbContext())
            {
                await context.RequestType.Delete(entity);
            }
        }
        #endregion

        #region List
        public async Task<List<RequestType>> GetAllAsync()
        {
            using (var context = new NeverForgetBotDbContext())
            {
                var result = await context.RequestType.List();
                return result.Result.ToList();
            }
        }
        #endregion
    }
}
