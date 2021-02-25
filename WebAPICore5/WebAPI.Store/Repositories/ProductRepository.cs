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
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;

        public ProductRepository(StoreContext context)
        {
            this.context = context;
        }
        public void Add()
        {
            var productList = new List<Product>
            {
                new Product
                {
                    Name="Camera",
                    Price = 100
                },
                new Product
                {
                    Name = "Laptop",
                    Price = 500
                },
                new Product
                {
                    Name = "TV",
                    Price = 300
                }
            };

            context.Products.AddRange(productList);
            context.SaveChanges();
        }

        public IList<Product> GetAllProduct()
        {
            var allProducts = context.Products.ToList();
            return allProducts;
        }

        public Product GetById(int id)
        {
            return context.Products.Where(x => x.Id == id).FirstOrDefault();

        }
    }
}
