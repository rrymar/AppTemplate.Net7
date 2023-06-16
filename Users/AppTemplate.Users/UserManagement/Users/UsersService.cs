using System.Linq;
using AppTemplate.Users.Database;
using Core.Database;
using Core.Database.Extensions;
using Core.Web.Errors;
using Microsoft.EntityFrameworkCore;

namespace AppTemplate.Users.UserManagement.Users
{
    public class UsersService
    {
        private readonly UsersDataContext dataContext;

        private readonly UserMapper mapper;

        public UsersService(UsersDataContext dataContext, UserMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public void Create(UserModel model)
        {
            var user = mapper.ToEntity(model);
            dataContext.Users.Add(user);

            user.Roles = model.Roles?.Select(e =>
            {
                return new UserRole()
                {
                    RoleId = e.RoleId
                };
            }).ToList();

            dataContext.SaveChanges();
            model.Id = user.Id;
        }

        public UserModel Get(int id)
        {
            var user = dataContext.Users.Where(u => u.Id == id)
                .Include(u => u.Roles).ThenInclude(r => r.Role)
                .SingleOrDefault();

            if (user == null)
                throw new NotFoundException();

            return mapper.ToModel(user);
        }

        public void Update(UserModel model)
        {
            var user = dataContext.Users.Find(model.Id);
            if (user == null)
                throw new NotFoundException();

            mapper.ToEntity(model, user);

            var changes = user.Roles.GetChanges(model.Roles, (e, m) => e.RoleId == m.RoleId);

            changes.RemoveDeleted(dataContext.UserRoles);
            changes.CreateAdded(dataContext.UserRoles,
                (m, e) =>
                {
                    e.RoleId = m.RoleId;
                    e.UserId = user.Id;
                });

            dataContext.SaveChanges();
        }

        public void Remove(int id)
        {
            if (KnownUsers.SystemUsers.Contains(id))
                throw new BusinessValidationException("System users can't be deleted.");

            var user = dataContext.Users.Find(id);
            if (user == null) return;

            dataContext.Users.Remove(user);
            dataContext.SaveChanges();
        }
    }
}
