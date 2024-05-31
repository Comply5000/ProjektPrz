﻿using API.Database.Context;
using API.Features.Identity.Config;
using API.Features.Identity.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class IdentityExtension
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfig = new AuthConfig();
            configuration.GetSection("Authentication").Bind(authConfig);
            services.AddSingleton(authConfig);
            
            ////Configure identity
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<EFContext>()
                .AddDefaultTokenProviders();

            //Configure it for production
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 4;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;

                // Email confirm
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            });


            //// configure DI for application services
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })    
            .AddGoogle(options =>
            {
                options.ClientId = configuration.GetValue<string>("GoogleAuth:ClientId")!;
                options.ClientSecret = configuration.GetValue<string>("GoogleAuth:ClientSecret")!;
            })
            .AddCookie(options =>
            {
                options.Cookie.Domain = "projekt-prz.comply.ovh";
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authConfig.JwtIssuer,
                    ValidAudience = authConfig.JwtIssuer,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.JwtKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            //Configure SecurityStamp object
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                //Configure how ofert securicy stamp should be validated
                options.ValidationInterval = TimeSpan.FromMinutes(30);
                options.OnRefreshingPrincipal = (context) => Task.CompletedTask;
            });
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
            });

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(1); // Set the token lifespan to 2 days
            });


            return services;
        }
    }
}
