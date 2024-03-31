using MG.RequestResponseMiddleware.FileLogger.Library;
using MG.RequestResponseMiddleware.Library;
using MG.RequestResponseMiddleware.Library.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;

namespace MiddlewareTestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(conf =>
            {
                conf.AddConsole(); // þimdilik console
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiddlewareTestApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddlewareTestApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.AddMGRequestResponseFileLoggerMiddleware(opt =>
            {
                opt.FileDirectory=AppDomain.CurrentDomain.BaseDirectory;
                opt.FileName = "Mursel_Log";
                opt.Extesion = "txt";
                opt.UseJsonFormat=true;
                opt.ForceCreateDirectory = true;    
            });

            //using the middleware
            //app.AddMGRequestResponseMiddleware(opt =>
            //{
            //    //opt.UseHandler(async context =>
            //    //{
            //    //    Console.WriteLine($"RequestBody: {context.RequestBody}");
            //    //    Console.WriteLine($"ResponseBody: {context.ResponseBody}");
            //    //    Console.WriteLine($"Timing: {context.FormattedCreationTime}");
            //    //    Console.WriteLine($"Url: {context.Url}");
            //    //});

            //    opt.UseLogger(app.ApplicationServices.GetRequiredService<ILoggerFactory>(), opt =>
            //    {
            //        opt.LogLevel = LogLevel.Error;
            //        opt.LoggerCategoryName = "MyCustomeCategoryName";
            //        opt.LoggingFields.Add(LogFileds.Request);
            //        opt.LoggingFields.Add(LogFileds.Response);
            //        opt.LoggingFields.Add(LogFileds.ResponseTiming);
            //        opt.LoggingFields.Add(LogFileds.Path);
            //        opt.LoggingFields.Add(LogFileds.QueryString);
            //    });

            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
