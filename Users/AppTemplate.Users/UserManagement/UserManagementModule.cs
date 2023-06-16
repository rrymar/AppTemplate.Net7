using AppTemplate.Users.UserManagement.Search;
using AppTemplate.Users.UserManagement.Users;
using Core.App;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.Users.UserManagement
{
    public class UserManagementModule : IModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddTransient<UserMapper>();

            services.AddTransient<UsersService>();
            services.AddTransient<SearchUsersService>();
        }
    }
}
