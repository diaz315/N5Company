using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UserPermission.Domain;

namespace UserPermission.Repository
{
    public class UserPermissionContext: DbContext
    {
        public virtual DbSet<PermissionType> PermissionTypes { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        public UserPermissionContext(DbContextOptions<UserPermissionContext> options) : base(options)
        {

        }
    }
}