using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiConsume.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsyn(string URL, int id);
        Task<IEnumerable<T>> GetAllAsync(string URL);
        Task<bool> CreateAsync(string URL, T obj);
        Task<bool> UpdateAsync(string URL, T obj);
        Task<bool> DeleteAsync(string URL, int id);
    }
}
