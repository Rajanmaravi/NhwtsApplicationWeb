using Microsoft.Extensions.DependencyInjection;
using Nhwts.Repository.Contract;
using Nhwts.Repository.Implementation;

namespace Nhwts.Web.Helper
{
    public static class ServiceRegistration
    {
        public static void ApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<ICaptchaRepository, CaptchaRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILoginRepository, LoginRepository>();
        }
    }
}
