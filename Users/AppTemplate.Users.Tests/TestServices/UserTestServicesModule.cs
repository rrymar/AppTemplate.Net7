using AppTemplate.Users.Tests.TestServices.UserManagement;
using Core.App;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.Users.Tests.TestServices
{
    public class UserTestServicesModule : IModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddTransient<IUserTestService, UserTestService>();
            services.AddTransient<SearchUsersTestService>();
        }
    }
}
