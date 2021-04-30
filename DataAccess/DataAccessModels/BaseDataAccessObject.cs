﻿using BlockBase.Dapps.NeverForget.Data.Context;
using BlockBase.Dapps.NeverForget.Data.Interfaces;
using BlockBase.Dapps.NeverForget.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.DataAccess.DataAccessModels
{
    public class BaseDataAccessObject<T> : IBaseDataAccessObject<T> where T : class
    {
        public async Task<IEnumerable<T>> List()
        {
            using var ctx = new NeverForgetBotDbContext();
            return await ctx.Set<T>().SelectAsync();
        }

        public async Task InsertAsync(T item)
        {
            using var ctx = new NeverForgetBotDbContext();
            await ctx.Set<T>().InsertAsync(item);
        }

        public async Task<T> GetAsync(Guid id)
        {
            using var ctx = new NeverForgetBotDbContext();
            return await ctx.Set<T>().GetAsync(id);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> condition)
        {
            using var ctx = new NeverForgetBotDbContext();
            return await ctx.Set<T>().Where(condition).SelectAsync();
        }


        public async Task UpdateAsync(T item)
        {
            using var ctx = new NeverForgetBotDbContext();
            await ctx.Set<T>().UpdateAsync(item);
        }

        public async Task DeleteAsync(T item)
        {
            using var ctx = new NeverForgetBotDbContext();
            await ctx.Set<T>().DeleteAsync(item);
        }

        //public async Task DeleteAsync(T item)
        //{
        //    using var ctx = new NeverForgetBotDbContext();
        //    await ctx.Set<T>().DeleteAsync();
        //}
    }
}
