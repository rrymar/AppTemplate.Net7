using Core.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Database
{
    public class FullAuditEntity : AuditEntity
    {
        public int CreatedById { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public User CreatedBy { get; set; }

        public int UpdatedById { get; set; }

        [ForeignKey(nameof(UpdatedById))]
        public User UpdatedBy { get; set; }
    }
}