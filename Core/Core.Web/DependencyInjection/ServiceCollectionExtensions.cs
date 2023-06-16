using Core.App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Web.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterModule<T>(this IServiceCollection services)
            where T : IModule, new()
        {
            var module = new T();
            module.Register(services);
        }

        public static void RegisterTopLevelModule(this IServiceCollection services,
            ITopLevelModule module,
            IMvcBuilder builder, IConfiguration configuration)
        {
            builder.AddApplicationPart(module.GetType().Assembly);
            module.Register(services, configuration);
        }
    }
}
