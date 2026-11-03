using Books.Data;
using Books.Extensions;
using Microsoft.EntityFrameworkCore;

var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");

if (File.Exists(envPath))
{
    DotNetEnv.Env.Load(envPath);
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

builder.AddDataContext();
builder.AddCrossOrigin();
builder.AddJwtToken();
builder.AddServices();
builder.AddAuthService();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbA = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbA.Database.Migrate();
}

app.UseCors(builder.Configuration["CORS_POLICY_NAME"] ?? string.Empty);

app.UseStaticFiles();

app.UseJsonExceptionHandler();

app.MapEndpoints();

app.Run();