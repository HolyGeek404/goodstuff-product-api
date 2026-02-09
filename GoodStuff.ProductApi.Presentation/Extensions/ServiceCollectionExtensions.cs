using Azure.Identity;
using GoodStuff.ProductApi.Application.Features.Product.Queries.GetByType;
using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Cosmos;
using Microsoft.Identity.Web;
using Microsoft.OpenApi;
using System.Reflection;
using GoodStuff.ProductApi.Infrastructure;

namespace GoodStuff.ProductApi.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public void AddCosmosRepoConfig(WebApplicationBuilder builder)
        {
            services.AddScoped(typeof(IReadRepository<>), typeof(CosmosRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(CosmosRepository<>));

            services.AddScoped<ICpuRepository, CpuRepository>();
            services.AddScoped<IGpuRepository, GpuRepository>();
            services.AddScoped<ICoolerRepository, CoolerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void AddMediatrConfig()
        {
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(GetByTypeQuery).Assembly));
        }

        public void AddAzureConfig(IConfigurationManager configuration)
        {
            var azureAd = configuration.GetSection("AzureAd");
        
            configuration.AddAzureKeyVault(new Uri(azureAd["KvUrl"]!), new DefaultAzureCredential());
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(azureAd);
        }

        public void AddDataBaseConfig(IConfigurationManager configuration)
        {
            services.AddSingleton(_ => new CosmosClient(configuration.GetConnectionString("CosmosDB")));
        }

        public void AddSwaggerConfig(IConfiguration configuration)
        {
            var tenantId = configuration.GetSection("AzureAd")["TenantId"];
            var swaggerScope = configuration.GetSection("Swagger")["Scope"];
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "GoodStuff Product API",
                    Version = "v1",
                    Description =
                        "Catalog and product data for GoodStuff commerce apps. " +
                        "All endpoints require a valid bearer token (OAuth2 auth code with PKCE). " +
                        "Use the configured scope to authenticate and explore product queries."
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "OAuth2.0 Auth Code with PKCE",
                    Name = "oauth2",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl =
                                new Uri($"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/authorize"),
                            TokenUrl =
                                new Uri(
                                    $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token"), //token end point
                            Scopes = new Dictionary<string, string> { { $"{swaggerScope}", "Swagger - Local testing" } }
                        }
                    }
                });
                c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecuritySchemeReference("oauth2", document), [swaggerScope!]
                    }
                });
            });
        }
    }
}
