using System.Threading.Tasks;
using FirstProject.Services;
using Microsoft.AspNetCore.Http;

namespace FirstProject
{
    public class WeatherEndpoint
    {
        public async Task Endpoint(HttpContext context, IResponseFormatter formatter)
        { 
            await formatter.Format(context, "Endpoint Class: It is cloudy in Milan");
        }
    }
}