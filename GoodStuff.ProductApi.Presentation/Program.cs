using Autofac.Extensions.DependencyInjection;
using Azure.Core;
using Azure.Identity;
using GoodStuff.ProductApi.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddCosmosRepoConfig(builder);
builder.Services.AddMediatRConfig();

TokenCredential credential;

if (builder.Environment.IsDevelopment())
{
    credential = new ClientSecretCredential(
        tenantId: builder.Configuration["AzureAd:TenantId"],
        clientId: builder.Configuration["AzureAd:ClientId"],
        clientSecret: builder.Configuration["AzureAd:ClientSecret"]
    );
}
else
{
    credential = new DefaultAzureCredential();
}

builder.Services.AddAzureConfig(builder.Configuration, credential);
builder.Services.AddServices();
builder.Services.AddDataBaseConfig(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GoodStuff Product Api v1");
        c.OAuthClientId(builder.Configuration["Swagger:SwaggerClientId"]);
        c.OAuthUsePkce();
        c.OAuthScopeSeparator(" ");
    }
);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();