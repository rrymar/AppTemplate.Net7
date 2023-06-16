using System;
using System.Collections.Generic;

namespace AppTemplate.Users.UserManagement
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public List<UserRoleModel> Roles { get; set; }

        public bool IsSystemUser { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
