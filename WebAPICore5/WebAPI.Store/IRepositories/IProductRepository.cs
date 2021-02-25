using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core;

namespace WebAPI.Store.IRepositories
{
    public interface IProductRepository
    {
        void Add();
        IList<Product> GetAllProduct();
        Product GetById(int id);
    }
}
