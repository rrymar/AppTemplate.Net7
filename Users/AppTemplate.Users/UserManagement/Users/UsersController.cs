using Microsoft.AspNetCore.Mvc;

namespace AppTemplate.Users.UserManagement.Users
{
    [ApiController]
    [Route(UsersRoutes.Users)]
    public class UsersController : ControllerBase
    {
        private readonly UsersService service;

        public UsersController(UsersService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public UserModel Get(int id)
        {
            return service.Get(id);
        }

        [HttpPost]
        public UserModel Post(UserModel user)
        {
            service.Create(user);
            return service.Get(user.Id);
        }

        [HttpPut("{id}")]
        public UserModel Put(int id, UserModel user)
        {
            user.Id = id;
            service.Update(user);
            return service.Get(user.Id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Remove(id);
        }
    }
}
