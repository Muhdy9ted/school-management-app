using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.OpenApi.Models;
using School_Management_App.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using School_Management_App.Helpers;
using Microsoft.IdentityModel.Tokens;

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

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
      }); ;
      
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

      // add our repository patterns to the services for injection into our controllers
      services.AddScoped<IAuthRepository, AuthRepository>();
      services.AddScoped<IStudentRepository, StudentRepository>();
      services.AddScoped<ICoursesRepository, CoursesRepository>();

      // Auto-mapper configuration
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new MappingProfile());
      });

      IMapper mapper = mappingConfig.CreateMapper();

      services.AddSingleton(mapper);

      // Authentication by JWT Token
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                  ValidateIssuer = false,
                  ValidateAudience = false
                };
              });

    }



    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      // configure global exception
      if (env.IsDevelopment())
      {
        //app.UseDeveloperExceptionPage();
        app.UseExceptionHandler(builder =>
        {
          builder.Run(async context =>
          {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = context.Features.Get<IExceptionHandlerFeature>();
            if (error != null)
            {
              context.Response.AddApplicationError(error.Error.Message);
              await context.Response.WriteAsync(error.Error.Message);
            }
          });
        });
      }
      else
      {
        app.UseExceptionHandler(builder =>
        {
          builder.Run(async context =>
          {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = context.Features.Get<IExceptionHandlerFeature>();
            if (error != null)
            {
              context.Response.AddApplicationError(error.Error.Message);
              await context.Response.WriteAsync(error.Error.Message);
            }
          });
        });
      }

      // adding swagger API to the request pipeline
      app.UseSwagger();

      // adding swagger API to the request pipeline
      app.UseSwaggerUI(options =>
          options.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management App API v1")
      );

      // use Cors
      app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
      app.UseAuthentication();
      app.UseMvc();
      app.Run(context =>
      {
        context.Response.Redirect("/swagger/index.html");
        return Task.CompletedTask;
      });
    }
  }
}
