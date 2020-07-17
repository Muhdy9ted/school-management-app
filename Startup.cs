using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;

namespace School_Management_App
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
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      // add swagger generator
      services.AddSwaggerGen(options =>
      {

        options.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "School Management App API",
          Description = "A list of all the API-endpoints that makes up the project",
          Contact = new OpenApiContact
          {
            Name = "Abdullahi Muhammed",
            Email = "muhammedu9ted@yahoo.com",
          },
        });
        var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"HosteliteAPI.xml";
        options.IncludeXmlComments(xmlPath);
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();

      app.UseSwaggerUI(options =>
          options.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management App API v1")
      );

      app.UseMvc();
    }
  }
}
