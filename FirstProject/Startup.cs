using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FirstProject
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(opts => { opts.CheckConsentNeeded = context => true; });

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });

            services.AddHsts(opts =>
            {
                opts.MaxAge = TimeSpan.FromDays(1);
                opts.IncludeSubDomains = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            
            //app.UseDeveloperExceptionPage();

            app.UseExceptionHandler("/error.html");

            if (env.IsProduction())
            {
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseStatusCodePages("text/html", Responses.DefaultResponse);

            app.UseCookiePolicy();

            app.UseMiddleware<ConsentMiddleware>();

            app.UseSession();

            

            //app.UseRouting();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/error")
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await Task.CompletedTask;
                }
                else
                {
                    await next();
                }
            });

            app.Run(context =>
            {
                throw new Exception("Something has gone wrong");
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/cookie", async context =>
            //    {
            //        int counter1 = (context.Session.GetInt32("counter1") ?? 0) + 1;
            //        int counter2 = (context.Session.GetInt32("counter2") ?? 0) + 1;
            //        context.Session.SetInt32("counter1", counter1);
            //        context.Session.SetInt32("counter2", counter2);
            //        await context.Session.CommitAsync();
            //        await context.Response.WriteAsync($"Counter1: {counter1}, Counter2: {counter2}");
            //    });

            //    endpoints.MapFallback(async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World");
            //    });
            //});
        }
    }
}
