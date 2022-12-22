using Microsoft.EntityFrameworkCore;

namespace UserPermission.Domain
{
    public class Permission
    {
        [Comment("Unique Id")]
        public int Id { get; set; }
        [Comment("Employee Forename")]
        public string? EmployeeForename { get; set; }
        [Comment("Employee Surname")]
        public string? EmployeeSurname { get; set; }
        [Comment("Permission granted on date")]
        public DateTime? PermissionDate { get; set; }
        public PermissionType? PermissionTypes { get; set; }
    }
}