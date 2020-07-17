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
using School_Management_App.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace School_Management_App
{
  /// <summary>
  /// the startup class 
  /// </summary>
  public class Startup
  {

    /// <summary>
    /// the startup class constructor
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }


    /// <summary>
    /// the configuration property
    /// </summary>
    public IConfiguration Configuration { get; }


    /// <summary>
    /// the configuration method for adding services to the project
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {

      // add the Dbcontext service
      services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      
      // add swagger generator
      services.AddSwaggerGen(options =>
      {

        options.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "School Management App API",
          Description = "A list of all the API-endpoints that makes up the School management app project",
          Contact = new OpenApiContact
          {
            Name = "Abdullahi Muhammed",
            Email = "muhammedu9ted@yahoo.com",
          },
        });
        var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"School Management App.xml";
        options.IncludeXmlComments(xmlPath);
      });

      // add Cors
      services.AddCors();

      // add our repositories to the services for injection into our controllers
      services.AddScoped<IAuthRepository, AuthRepository>();

      // Auto-mapper configuration
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new MappingProfile());
      });

      IMapper mapper = mappingConfig.CreateMapper();

      services.AddSingleton(mapper);
    }



    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // adding swagger API to the request pipeline
      app.UseSwagger();

      // adding swagger API to the request pipeline
      app.UseSwaggerUI(options =>
          options.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management App API v1")
      );

      // use Cors
      app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());

      app.UseMvc();
    }
  }
}
