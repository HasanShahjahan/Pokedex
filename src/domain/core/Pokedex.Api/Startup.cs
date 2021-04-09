using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pokedex.DataObjects.Settings;
using Pokedex.Domain.Interfaces;
using Pokedex.Domain.Managers;
using Pokedex.Domain.Services;
using Pokedex.External.Interface.CustomService;
using Pokedex.External.Interface.RestClient;
using Pokedex.External.Interface.RestClientService;
using Pokedex.Infrastructure.Repository;
using Pokedex.Security.Handlers;
using Pokedex.Validator;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Api
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var settings = GetAppConfigurationSection();
            services.AddControllers();
            ConfigureSwaggerServices(services);
            ConfigureSingletonServices(services);
            ConfigureTransientServices(services);
            ConfigureJwtAuthentication(services, settings);
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokedex API");

            });
        }

        private AppSettings GetAppConfigurationSection()
        {
            return Configuration.GetSection("AppSettings").Get<AppSettings>();
        }

        private void ConfigureSingletonServices(IServiceCollection services)
        {
            var settings = GetAppConfigurationSection();
            services.AddSingleton(settings);
        }

        private void ConfigureTransientServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IValidator), typeof(PayloadValidator));
            services.AddTransient(typeof(IJwtTokenHandler), typeof(JwtTokenHandler));
            services.AddTransient(typeof(IAutoRefreshingCacheService), typeof(AutoRefreshingCacheService));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IPokemonService), typeof(PokemonService));
            services.AddTransient(typeof(IPokemonRepository), typeof(PokemonRepository));
            services.AddTransient(typeof(IPokemonManager), typeof(PokemonManager));
            services.AddTransient(typeof(IRestClientHandler), typeof(RestClientHandler));
            services.AddTransient(typeof(ITranslatedPokemon), typeof(TranslatedPokemon));
        }

        private static void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Swagger API",
                        Description = "Pokedex API",
                        Version = "v1"
                    });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

            });
        }

        private void ConfigureJwtAuthentication(IServiceCollection services, AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.JsonWebTokens.Secret);
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
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            IdentityModelEventSource.ShowPII = true;
        }
    }
}
