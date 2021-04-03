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
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public Product GetProductById(int id)
        {
            return productRepository.GetById(id);
        }

        public IList<Product> GetProducts()
        {
            return productRepository.GetAllProduct();
        }

        public void AddProducts(IEnumerable<Product> products)
        {
            productRepository.Add(products);

        }
    }
}
