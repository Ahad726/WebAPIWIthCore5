using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core;
using WebAPI.Store.Context;
using WebAPI.Store.IRepositories;

namespace WebAPI.Store.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreContext context;

        public UserRepository(StoreContext context)
        {
            this.context = context;
        }
        public User GetUserByEmail(string email)
        {
            
            var user =  context.Users.Include(user => user.Role).FirstOrDefault(user => user.Email == email);
            return user;

            //return context.Users.Where(u => u.Email == email).FirstOrDefault();


        }

        public void Registeruser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
