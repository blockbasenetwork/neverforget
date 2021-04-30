﻿using BlockBase.Dapps.NeverForget.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.DataAccess.Interfaces
{
    public interface IRequestTypeDataAccessObject
    {
        Task InsertAsync(RequestType entity);
        Task<RequestType> GetAsync(int id);
        Task<List<RequestType>> GetAllAsync();
        Task UpdateAsync(RequestType entity);
        Task HardDeleteAsync(RequestType entity);
    }
}