using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core;

namespace WebAPI.Store.IServices
{
    public interface IProductService
    {
        IList<Product> GetProducts();
        Product GetProductById(int id);
        void AddProducts(IEnumerable<Product> products);
    }
}
