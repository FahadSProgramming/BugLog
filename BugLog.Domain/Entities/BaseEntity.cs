using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugLog.Domain.Entities
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }    
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public SystemUser CreatedBy { get; set; }
        public Guid? CreatedById { get; set; }
        public SystemUser ModifiedBy { get; set; }
        public Guid? ModifiedById { get; set; }
    }
}