using BlockBase.Dapps.NeverForget.Data.Context;
using BlockBase.Dapps.NeverForget.Data.Entities.Base;
using BlockBase.Dapps.NeverForget.Data.Interfaces;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.DataAccess.DataAccessModels
{
    public class GenericDataAccessObject : IGenericDataAccessObject
    {
        public async Task<IEnumerable<T>> ListAsync<T>() where T : class
        {
            using var ctx = new NeverForgetBotDbContext();
            return await ctx.Set<T>().SelectAsync();
        }

        public async Task InsertAsync<T>(T item) where T : class
        {
            using var ctx = new NeverForgetBotDbContext();
            await ctx.Set<T>().InsertAsync(item);
        }

        public async Task<T> GetAsync<T>(Guid id) where T : class
        {
            using var ctx = new NeverForgetBotDbContext();
            return await ctx.Set<T>().GetAsync(id);
        }

        public async Task<IEnumerable<T>> Find<T>(Expression<Func<T, bool>> condition) where T : class
        {
            using var ctx = new NeverForgetBotDbContext();
            return await ctx.Set<T>().Where(condition).SelectAsync();
        }

        public async Task UpdateAsync<T>(T item) where T : class
        {
            using var ctx = new NeverForgetBotDbContext();
            await ctx.Set<T>().UpdateAsync(item);
        }

        public async Task DeleteAsync<T>(T item) where T : class
        {
            using var ctx = new NeverForgetBotDbContext();
            await ctx.Set<T>().DeleteAsync(item);
        }

        public async Task DeleteAsync<T>(Guid id) where T : class
        {
            using var ctx = new NeverForgetBotDbContext();
            await ctx.Set<T>().DeleteAsync();
        }

        public async Task<int> Count<T>(Expression<Func<T, bool>> condition) where T : class
        {
            using var ctx = new NeverForgetBotDbContext();
            return (await ctx.Set<T>().Where(condition).SelectAsync()).Count();
        }

        public async Task<bool> Any<T>(Expression<Func<T, bool>> condition) where T : class
        {
            using var ctx = new NeverForgetBotDbContext();
            return (await ctx.Set<T>().Where(condition).SelectAsync()).Any();
        }

        public async Task<T> FirstOrDefault<T>(Expression<Func<T, bool>> condition) where T : class
        {
            using var ctx = new NeverForgetBotDbContext();
            var list = (await ctx.Set<T>().Where(condition).SelectAsync()).ToArray();
            return (!list.Any()) ? default : list[0];
        }
    }
}
