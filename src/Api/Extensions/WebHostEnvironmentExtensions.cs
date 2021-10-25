using Microsoft.AspNetCore.Hosting;

namespace Ecommerce.Services.Orders.Api.Extensions
{
    public static class WebHostEnvironmentExtenions
    {
        public static bool IsLocal(this IWebHostEnvironment env)
        {
            return env.EnvironmentName?.ToLower() == "local";
        }
    }
}
