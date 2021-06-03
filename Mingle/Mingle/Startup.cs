using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Mingle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mingle
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
            if (null == MingleSerializer.DeserializeAvatars())
            {
                services.AddSingleton<IAvatarsRepository,AvatarsRepository>();
            }
            else
            {
                services.AddSingleton<IAvatarsRepository>((IAvatarsRepository)MingleSerializer.DeserializeAvatars());
            }
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Title = "Mingle", 
                    Version = "v1",
                    Description = "Mingle assignment to demonstrate Avatar API",
                    Contact = new OpenApiContact
                    {
                        Name = "Lyudmil Pashayanov",
                        Email = "sytix1@hotmail.com",
                        Url = new Uri("https://www.lyudmilpashayanov.com"),
                    },

                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // This is to enable middleware to serve generated Swagger as a JSON endpoint
                app.UseSwagger();
                // This is to enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mingle v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
