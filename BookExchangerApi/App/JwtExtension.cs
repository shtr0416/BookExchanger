using BookExchangerApi.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BookExchangerApi.App
{
    public static class JwtExtension
    {
        public static void JwtRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.Audience = configuration[$"JWTConfig:{nameof(JwtConfig.Audience)}"];

                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration[$"JWTConfig:{nameof(JwtConfig.Issuer)}"],

                    ValidateAudience = true,
                    ValidAudience = configuration[$"JWTConfig:{nameof(JwtConfig.Audience)}"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration[$"JWTConfig:{nameof(JwtConfig.IssuerSigningKey)}"])),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                opt.SecurityTokenValidators.Clear();
                opt.SecurityTokenValidators.Add(new AuthService());
            });
        }
    }
}