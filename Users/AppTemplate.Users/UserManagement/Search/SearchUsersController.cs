using System.Threading;
using Core.Web.Crud;
using Microsoft.AspNetCore.Mvc;

namespace AppTemplate.Users.UserManagement.Search
{
    [ApiController]
    [Route(UsersRoutes.SearchUsers)]
    public class SearchUsersController : ControllerBase
    {
        private readonly SearchUsersService service;

        public SearchUsersController(SearchUsersService service)
        {
            this.service = service;
        }

        [HttpPost]
        public ResultsList<UserModel> Post([FromBody] SearchQuery query)
        {
            return service.Search(query);
        }
    }
}
