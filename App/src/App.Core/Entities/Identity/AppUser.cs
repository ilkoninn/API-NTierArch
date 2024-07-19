using App.Core.Entities.Commons;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Entities.Identity
{
    public class AppUser : IdentityUser, IAuditedEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDisabled { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
