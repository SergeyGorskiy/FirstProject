using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FirstProject
{
    public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
            {
                await context.Response.WriteAsync("Class-based Middleware \n ");
            }

            await _next(context);
        }
    }

    public class LocationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MessageOptions _options;

        public LocationMiddleware(RequestDelegate next, IOptions<MessageOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/location")
            {
                await context.Response.WriteAsync($"{_options.CityName}, {_options.CountryName}");
            }
            else
            {
                await _next(context);
            }
        }
    }
}