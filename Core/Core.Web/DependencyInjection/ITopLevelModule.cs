using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Web.DependencyInjection
{
    public interface ITopLevelModule
    {
        void Register(IServiceCollection services, IConfiguration configuration);
    }
}
