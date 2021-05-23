using Dapper;
using EntityFramework.Intro.Models;
using EntityFramework.Intro.Models.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Intro.Middleware
{
    public class Test
    {
        private RequestDelegate nextDelegate;

        public Test(RequestDelegate next)
        {
            nextDelegate = next;
        }

        public async Task Invoke(HttpContext context,IConfiguration configuration, DataContext dataContext)
        {
            if (context.Request.Path == "/test")
            {
                await context.Response.WriteAsync(
                $"There are {dataContext.Products.Count()} products\n");
                await context.Response.WriteAsync(
                $"There are {dataContext.Categories.Count()} categories\n");
                await context.Response.WriteAsync(
                $"There are {dataContext.Suppliers.Count()} suppliers\n");
            }

            //

            else if (context.Request.Path == "/dapper")
            {
                // Exercise:
                // Bereitstellung der Dapper Instanzen durch DepenedencyInjection

                string connectionString = configuration["ConnectionStrings:DefaultConnection"];
                string sqlStatement = "SELECT TOP 10 * FROM Products;";
                sqlStatement = "SELECT * FROM Products;";

                using (var connection = new SqlConnection(connectionString))
                {
                    IEnumerable<Product> productsByDapperDirect = connection.QueryAsync<Product>(sqlStatement).Result.ToList();

                    await context.Response.WriteAsync($"Dapper (direct) - with {connection}:\n");
                    foreach (Product product in productsByDapperDirect)
                    {
                        await context.Response.WriteAsync($"   Product: {product.Name}\n");
                    }
                }
            }
            else
            {
                await nextDelegate(context);
            }
        }
    }

}
    

