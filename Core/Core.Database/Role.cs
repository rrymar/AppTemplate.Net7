using System.ComponentModel.DataAnnotations;

namespace Core.Database
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }
    }

    public static class KnownRoles
    {
        public const string SystemAdministrator = "SystemAdministrator";
    }
}