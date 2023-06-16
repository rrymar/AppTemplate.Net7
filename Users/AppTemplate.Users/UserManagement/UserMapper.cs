using System.Linq;
using Core.Database;
using Core.Web.Crud;

namespace AppTemplate.Users.UserManagement
{
    public class UserMapper : IMapper<User, UserModel>
    {
        public User ToEntity(UserModel model, User entity = null)
        {
            if (entity == null)
                entity = new User();

            entity.Username = model.Username;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;

            return entity;
        }

        public UserModel ToModel(User entity)
        {
            return new UserModel()
            {
                Id = entity.Id,
                Username = entity.Username,
                FullName = entity.FullName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                IsSystemUser = KnownUsers.SystemUsers.Contains(entity.Id),
                CreatedOn = entity.CreatedOn,
                Roles = entity.Roles?.Select(e => new UserRoleModel()
                {
                    RoleId = e.RoleId,
                    RoleName = e.Role.Name
                }).ToList()
            };
        }
    }
}
