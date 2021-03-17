using BookExchanger.Service;
using BookExchanger.Service.Services;
using BookExchangerApi.Jwt;
using BookExchangerApi.Session;
using Microsoft.Extensions.DependencyInjection;

namespace BookExchangerApi.App
{
    public static class DependencyExtension
    {
        public static void DependencyRegister(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddHttpContextAccessor();
            services.AddTransient<IUserSession, UserSession>();
        }
    }
}