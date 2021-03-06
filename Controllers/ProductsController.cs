using EntityFramework.Intro.Models;
using EntityFramework.Intro.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Intro.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private DataContext context;
        public ProductsController(DataContext ctx)
        {
            context = ctx;
        }
       [HttpGet]
       public IAsyncEnumerable<Product> GetProducts()
        {
            /*
            return new Product[]
            {
            new Product() { Name= "Product #1"},
            new Product() { Name = "Product #1" },
        };
            */
            return context.Products;
        }
       [HttpGet("{id}")]
       public async Task <Product> GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        {
            /*
            return new Product()
            {
                ProductId = 1,
                Name = "Test Product"
            };
            */
            logger.LogDebug("GetProduct Action Invoked");
            return await context.Products.FindAsync(id);
        }
        [HttpPost]
        public async Task SaveProduct([FromBody] Product product)
        {
           await context.Products.AddAsync(product);
           await context.SaveChangesAsync();
        }
        [HttpPut]
        public async Task UpdateProduct([FromBody] Product product)
        {
            context.Products.Update(product);
           await context.SaveChangesAsync();
        }
        [HttpDelete("{id}")]
        public async Task DeleteProduct(long id)
        {
            context.Products.Remove(new Product() { ProductId = id });
          await context.SaveChangesAsync();
        }
    }
}
