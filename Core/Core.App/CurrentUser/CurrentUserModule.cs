using Microsoft.Extensions.DependencyInjection;

namespace Core.App.CurrentUser
{
    public class CurrentUserModule : IModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<ICurrentUserLocator, UserContext>();
        }
    }
}
