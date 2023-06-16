using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Database
{
    public interface IAuditEntity
    {
        DateTime CreatedOn { get; set; }

        DateTime UpdatedOn { get; set; }
    }

    public class AuditEntity : IDeactivatable, IAuditEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }

    public static class AuditEntityExtensions
    {
        public static void Deactivate(this AuditEntity entity)
        {
            entity.IsActive = false;
            entity.UpdatedOn = DateTime.UtcNow;
        }
    }
}