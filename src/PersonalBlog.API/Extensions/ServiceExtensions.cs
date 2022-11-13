using System.Collections.Immutable;
using System;
using System.Buffers;
using System.Globalization;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using PersonalBlog.API.Settings;
using PersonalBlog.Business.Models.Validations;
using PersonalBlog.Business.Services.Abstract;
using PersonalBlog.Business.Services.Concrete;
using PersonalBlog.DataAccess.Entities.Concrete;
using PersonalBlog.DataAccess.Repositories.Abstract.Interfaces;
using PersonalBlog.DataAccess.Repositories.Concrete;

namespace PersonalBlog.API.Extensions;

public static class ServiceExtensions
{
    private static IConfiguration _configuration;
    public static MongoDbSettings Settings 
    { 
        get 
        {
            if (_configuration is null)
            {
                throw new ArgumentNullException(nameof(_configuration), "Before using the extension class please make sure Init method called first.");
            }
            return _configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
        }
    } 

    public static void Init(this IServiceCollection collection, IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static void AddDependencyInjections(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(serviceProvider => new MongoClient(Settings.ConnectionString));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
    }

    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IValidationsMarker>();
    }

    public static void AddIdentityExtension(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser,ApplicationRole>(options => 
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireUppercase = false;
        }).AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(Settings.ConnectionString, Settings.DatabaseName);
    }

    public static void AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => 
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "PersonalBlog API", Version = "v1"});

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. (Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void AddAuthenticationAndAuthorization(this   IServiceCollection services)
    {
        services.Configure<JwtConfig>(_configuration.GetSection(nameof(JwtConfig)));
        byte[]? key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, //for development
            ValidateAudience = false, //for development
            RequireExpirationTime = false, //for development
            ValidateLifetime = true
            //ValidIssuer = builder.Configuration.GetSection("JwtConfig:Issuer").Value
        };

        services.AddAuthentication( c => 
        {
            c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer( jwt => 
        {
            jwt.RequireHttpsMetadata = true;
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = tokenValidationParameters;
        });

        services.AddAuthorization( options => {
           options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin")); 
        });

        services.AddSingleton(tokenValidationParameters);
        services.AddScoped<IAuthService, AuthService>();
    }
}