using Autofac.Extensions.DependencyInjection;
using GoodStuff.ProductApi.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddCosmosRepoConfig(builder);
builder.Services.AddMediatrConfig();
builder.Services.AddAzureConfig(builder.Configuration);
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