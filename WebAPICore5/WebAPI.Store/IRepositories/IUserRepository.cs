using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core;

namespace WebAPI.Store.IRepositories
{
    public interface IUserRepository
    {
        void Registeruser(User user);
        User GetUserByEmail(string email);
    }
}
