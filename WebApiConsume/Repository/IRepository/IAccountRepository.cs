using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiConsume.Models;

namespace WebApiConsume.Repository.IRepository
{
    public interface IAccountRepository:IRepository<User>
    {
        Task<bool> RegisterAsync(string URL, User userModel);
        Task<User> LoginAsync(string URL, User userModel);
    }
}
