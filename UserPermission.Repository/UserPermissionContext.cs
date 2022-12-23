using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UserPermission.Domain;

namespace UserPermission.Repository
{
    public class UserPermissionContext: DbContext
    {
        public virtual DbSet<PermissionType> PermissionTypes { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured){
                var db = "\"Server=JOSEDIAZ;Database=Permission_1;Trusted_Connection=True;MultipleActiveResultSets=true;Trust Server Certificate=true\"";
                optionsBuilder.UseSqlServer(db);
            }
        }

        public UserPermissionContext()
        {

        }
        
        public UserPermissionContext(DbContextOptions<UserPermissionContext> options) : base(options)
        {

        }
    }
}