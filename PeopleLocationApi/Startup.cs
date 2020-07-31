using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using PeopleLocationApi.Models;
using PeopleLocationApi.Services;

namespace PeopleLocationApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IConfigurationRoot configuration =
                new ConfigurationBuilder()
                    .SetBasePath(_env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .Build();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "People Location API", Version = "V1"});
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddHttpClient<IBpdtsTestAppService, BpdtsTestAppService>(client =>
            {
                client.BaseAddress =
                    new Uri(configuration.GetSection("AppSettings").GetSection("BpdtsTestAppBaseApiUri").Value);
                client.DefaultRequestHeaders.Add("accept", "application/json");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "People Location API V1"); });
        }
    }
}