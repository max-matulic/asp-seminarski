using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

namespace Projekt.Data.Migrations
{
    public partial class AdminDataAdd : Migration
    {
        const string _adminUserGuid = "320c2bec-62c8-4f0e-b25a-b5c9b118a336";
        const string _adminRoleGuid = "c358e505-c4ce-4eb9-a6bd-daabd39b831c";



        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var passwordHashed = hasher.HashPassword(null, "Password12345");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO AspNetUsers(Id, UserName, NormalizedUserName, Email, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, NormalizedEmail, PasswordHash, SecurityStamp, FirstName)");
            sb.AppendLine("VALUES(");
            sb.AppendLine($"'{_adminUserGuid}'");
            sb.AppendLine(", 'admin@admin.com'");
            sb.AppendLine(", 'ADMIN@ADMIN.COM'");
            sb.AppendLine(", 'admin@admin.com'");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 0");
            sb.AppendLine(", 'ADMIN@ADMIN.COM'");
            sb.AppendLine($", '{passwordHashed}'");
            sb.AppendLine(", ''");
            sb.AppendLine(", 'Admin'");
            sb.AppendLine(")");

            migrationBuilder.Sql(sb.ToString());
            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES('{_adminRoleGuid}', 'Admin', 'ADMIN')");
            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles(UserId, RoleId) VALUES ('{_adminUserGuid}', '{_adminRoleGuid}')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId = '{_adminUserGuid}' AND RoleId = '{_adminRoleGuid}'");
            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id='{_adminRoleGuid}'");
            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id='{_adminUserGuid}'");
        }
    }
}
