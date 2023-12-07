using Microsoft.AspNetCore.Identity;
using ReDoMusic.Domain.Entities;
using ReDoMusic.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReDoMusic.Domain.Identity
{
    public class User:IdentityUser<Guid>,IEntityBase<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public UserSetting UserSetting { get; set; }
    }
}
