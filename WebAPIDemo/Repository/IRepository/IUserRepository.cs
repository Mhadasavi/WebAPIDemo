using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIDemo.Models;

namespace WebAPIDemo.Repository.IRepository
{
    public interface IUserRepository
    {
        bool isUserExist(string username);
        User Authenticate(string username, string password);
        User Register(string username, string password);
    }
}
