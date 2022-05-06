using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interface;
using dal =  DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DAL.Repository;
using LocalModel.Services.Interface;
using LocalModel.Services;
using LocalModel.Models;
using System.IO;
using IMDBApi.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Permissions;
using Microsoft.Extensions.FileProviders;

namespace IMDBApi
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
            services.AddControllers();

            services.AddCors();

            #region Repo
            services.AddScoped<IUserRepository<dal.User>, UserRepo>();
            services.AddScoped<IMovieRepository<dal.Movie, dal.Actor>, MovieRepo>();
            services.AddScoped<IPersonRepository<dal.Person, dal.ActIn>, PersonRepo>();
            services.AddScoped<ICommentRepository<dal.Comment>, CommentRepo>();
            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ICommentService<Comment>, CommentService>();
            #endregion

            

            #region ConfigSwagger
            services.AddSwaggerGen(c => 
            {
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "swagger.xml");
                c.IncludeXmlComments(filePath);
                c.DescribeAllParametersInCamelCase();
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "IMDB API",
                    Description = "Base de données de film",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "steve.lorent@bstorm.be",
                        Name = "Steve"
                    }
                });
            });
            #endregion

            #region Config JWToken
            //Récupération de la clé "secret"
            IConfigurationSection monSecret = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(monSecret);

            //Config de l'authentification

            AppSettings appSettings = monSecret.Get<AppSettings>();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);

            //Définition des autorisations selon les rôles
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("User", policy => policy.RequireRole("User", "Admin"));
            });
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:53448",
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

            services.AddScoped<ITokenService, TokenService>();
            
            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //    Path.Combine(env.ContentRootPath, "image")),
            //    RequestPath = "/image"
            //});
            app.UseSwaggerUI(c =>
            {
                c.DisplayOperationId();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API");
            });
        }
    }
}
