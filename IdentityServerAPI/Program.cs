using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using MisUserManagement.Endpoints.API.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.Extensions.DependencyInjection;
using IdentityServerAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options =>
//    {
//        options.Authority = "https://localhost:5001";

//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false
//        };
//    });

var configuration = builder.Configuration;

builder.Services
                .UseSql(configuration)
                .AddScopeConfigure()
                .AddIdentityServerService(configuration)
                .AddSwaggerService(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var swaggerOption = configuration.GetSection("Swagger").Get<SwaggerOption>();
    if (swaggerOption != null && swaggerOption.SwaggerDoc != null)
    {
        app.UseSwagger();
        app.UseSwaggerUI(delegate (SwaggerUIOptions c)
        {
            c.SwaggerEndpoint(swaggerOption.SwaggerDoc.URL, swaggerOption.SwaggerDoc.Title);
            // c.RoutePrefix = string.Empty;
            c.OAuthUsePkce();
        });
    }
}
app.UseCors(delegate (CorsPolicyBuilder builder)
{
    builder.AllowAnyOrigin();
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.WithExposedHeaders("Content-Disposition");
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
