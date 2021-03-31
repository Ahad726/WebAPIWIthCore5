using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Core;
using WebAPI.Store.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPICore5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            
            return productService.GetProducts();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return productService.GetProductById(id);

        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
