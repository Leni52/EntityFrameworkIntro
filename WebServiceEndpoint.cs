﻿using EntityFramework.Intro.Models;
using EntityFramework.Intro.Models.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace Microsoft.AspNetCore.Builder
{
    public static class WebServiceEndpoint
    {
        private static string BASEURL = "api/products";
        public static void MapWebService(this IEndpointRouteBuilder app)
        {
            app.MapGet($"{BASEURL}/{{id}}", async context =>
            {
                long key = long.Parse(context.Request.RouteValues["id"] as string);
                DataContext data = context.RequestServices.GetService<DataContext>();
                Product p = data.Products.Find(key);
                if (p == null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize<Product>(p));
                }
            });

            app.MapGet(BASEURL, async context =>
            {
                DataContext data = context.RequestServices.GetService<DataContext>();
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize<IEnumerable<Product>>(data.Products));
            });

            app.MapPost(BASEURL, async context =>
            {
                DataContext data = context.RequestServices.GetService<DataContext>();
                Product p = await JsonSerializer.DeserializeAsync<Product>(context.Request.Body);
                await data.AddAsync(p);
                await data.SaveChangesAsync();
                context.Response.StatusCode = StatusCodes.Status200OK;
              });
        }
    }
}
