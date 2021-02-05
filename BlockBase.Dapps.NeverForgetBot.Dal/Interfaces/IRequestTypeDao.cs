using BlockBase.Dapps.NeverForgetBot.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.Dal.Interfaces
{
    public interface IRequestTypeDao
    {
        Task InsertAsync(RequestType entity);
        Task<RequestType> GetAsync(int id);
        Task<List<RequestType>> GetAllAsync();
        Task UpdateAsync(RequestType entity);
        Task HardDeleteAsync(RequestType entity);
    }
}
