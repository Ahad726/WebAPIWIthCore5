using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebAPI.Core;
using WebAPI.Store.IServices;
using WebAPICore5.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPICore5.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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
        [AllowAnonymous]
        public IEnumerable<Product> Get()
        {
            return productService.GetProducts();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [Authorize(Policy = "HasNationality")]   // Authorized user have this particular claim can access.
        public Product Get(int id)
        {
            return productService.GetProductById(id);

        }

        // POST api/<ProductController>
        [HttpPost]
        [Authorize(Roles ="Admin, Moderator")]  // the authorized user who have Admin and moderator role , can access.
        public void Post([FromBody] IList<ProductModel> products)
        {
            productService.AddProducts(products);
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
