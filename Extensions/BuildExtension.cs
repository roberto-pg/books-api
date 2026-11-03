using System.Text;
using Books.Data;
using Books.Interfaces.Services;
using Books.Interfaces.Services.Books;
using Books.Interfaces.Services.Users;
using Books.Services.Books;
using Books.Services.Users;
using Books.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Books.Extensions;

public static class BuildExtension
{
    public static void AddDataContext(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;
        var connectionString = config["CONNECTION_STRING"];
        builder
            .Services
            .AddDbContext<AppDbContext>(dbCtx => { dbCtx.UseNpgsql(connectionString); });
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;

        var policyName = config["CORS_POLICY_NAME"];

        var origins =
            config["CORS_ORIGINS"]
                ?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ??
            ["http://localhost:3000"];

        var exposedHeader = config["CORS_EXPOSED_HEADERS"];

        builder.Services.AddCors(options =>
            options.AddPolicy(policyName!, policy =>
                policy
                    .WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders(exposedHeader!)));
    }

    public static void AddJwtToken(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;
        var jwtKey = config["JWT_KEY"];
        var jwtIssuer = config["JWT_ISSUER"];
        var jwtAudience = config["JWT_AUDIENCE"];

        if (jwtKey != null)
        {
            var key = Encoding.UTF8.GetBytes(jwtKey);
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtIssuer,

                        ValidateAudience = true,
                        ValidAudience = jwtAudience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtKey!)),

                        ValidateLifetime = true
                    };

                    // 🔥 AQUI é a mágica
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["access_token"];
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }

    public static void AddAuthService(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddScoped<AuthService>();
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder
            .Services.AddTransient<ICreateUser, CreateUser>();

        builder
            .Services.AddTransient<IAuthenticate, Authenticate>();

        builder
            .Services.AddTransient<IGetAllUsers, GetAllAllUsers>();

        builder
            .Services.AddTransient<IDeleteUser, DeleteUser>();

        builder
            .Services.AddTransient<ICreateBook, CreateBook>();

        builder
            .Services.AddTransient<IGetAllBooks, GetAllBooks>();

        builder
            .Services.AddTransient<IGetBookById, GetBookById>();

        builder
            .Services.AddTransient<IGetBooksByStatus, GetBooksByStatus>();

        builder
            .Services.AddTransient<IUpdateBookStatus, UpdateBookStatus>();

        builder
            .Services.AddTransient<IDeleteBook, DeleteBook>();

        builder
            .Services.AddTransient<IUpdateBookImage, UpdateBookImage>();
    }
}