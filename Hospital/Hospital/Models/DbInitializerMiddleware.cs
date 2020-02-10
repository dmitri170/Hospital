using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Data;


namespace Hospital.Models
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;
            public DbInitializerMiddleware(RequestDelegate next)
            {
                _next = next;

            }
            public Task Invoke(HttpContext context, HospitalContext dbContext)
            {
                DbInitializer.Initialize(dbContext);
                return _next.Invoke(context);

            }
        }
        public static class DbInitializerExtensions
        {
            public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<DbInitializerMiddleware>();
            }
        }
    }
