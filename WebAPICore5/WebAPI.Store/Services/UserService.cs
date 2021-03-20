using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core;
using WebAPI.Store.IRepositories;
using WebAPI.Store.IServices;

namespace WebAPI.Store.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public User GetUser(string email)
        {
            return userRepository.GetUserByEmail(email);

        }

        public void Register(User user)
        {
            userRepository.Registeruser(user);

        }
    }
}
