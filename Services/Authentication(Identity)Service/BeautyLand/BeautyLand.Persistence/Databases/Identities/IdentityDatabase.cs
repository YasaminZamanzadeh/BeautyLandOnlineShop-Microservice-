using BeautyLand.Domain.Role;
using BeautyLand.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Persistence.Databases.IdentityServer
{
    public class IdentityDatabase : IdentityDbContext<User, Role, string>
    {
        public IdentityDatabase(DbContextOptions<IdentityDatabase> options) : base(options)
        {

        }
    }
}
