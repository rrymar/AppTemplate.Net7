using Microsoft.Extensions.DependencyInjection;

namespace Core.App
{
    public interface IModule
    {
        void Register(IServiceCollection services);
    }
}
