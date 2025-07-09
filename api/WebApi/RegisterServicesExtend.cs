using Api.UserStories.domain;
using infrastructure.DataBase;
using infrastructure.jwt;
using Interfaces.UserStory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserStories.login;

namespace WebApi
{
    public static class RegisterServicesExtend
    {
        public static void RegisterServices(this IServiceCollection services,
           IConfiguration configuration)
        {

            if (services == null || configuration == null)
                return;

            services.RegisterDataBase(configuration, "ConnectionStrings:DefaultConnection", 1024);
            services.AddHttpContextAccessor();


            #region Autoirization & autentication 


            // Configuración de autenticación y autorización
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
            
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Headers.ContainsKey("x-token"))
                        {
                            context.Token = context.Request.Headers["x-token"];
                        }
                        return Task.CompletedTask;
                    }
                };
            });
                /*
                    services.AddAuthentication("CustomTokenScheme")
                    .AddScheme<AuthenticationSchemeOptions, CustomTokenAuthenticationHandler>("CustomTokenScheme", null);


                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

                */
                /*
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "CustomTokenScheme";
                    options.DefaultChallengeScheme = "CustomTokenScheme";
                })
                .AddJwtBearer("CustomTokenScheme", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });
                */


                services.AddAuthorization(options =>
            {
                options.AddPolicy("CreatePolicy", policy =>
                policy.RequireClaim("salesAdd", "True"));

            });
            #endregion

            #region Services & bussiness logic 
            // services (no functional req)
            services.AddScoped<IMyTokenGenerator, MyTokenGenerator>();

            // business logic
            services.AddTransient<ItokenUserStory, TokenUserStory>();
            services.AddTransient<IsalesUserStory, SalesUserStory>();
            services.AddTransient<ICatalogUserStory, CatalogUserStory>();
            //  services.AddTransient<IemployeeUserStory, employeeUserStory>();
            #endregion
        }

    }
}
