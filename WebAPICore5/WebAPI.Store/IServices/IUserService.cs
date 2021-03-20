using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core;

namespace WebAPI.Store.IServices
{
    public interface IUserService
    {
        void Register(User user);
        User GetUser(string email);
    }
}
