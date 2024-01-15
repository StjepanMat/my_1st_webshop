using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace Webshop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminAccount : Migration
    {
        const string ADMIN_USER_GUID = "ecded474-3395-4bc8-b74f-badd4ead77ba";
        const string ADMIN_ROLE_GUID = "0d08ab77-a334-44c2-82ae-1438ad7ad473";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var passwordhash = hasher.HashPassword(null, "Admin123!");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO AspNetUsers (Id, " +
                                        "UserName, " +
                                        "NormalizedUserName, " +
                                        "Email, " +
                                        "NormalizedEmail, " +
                                        "EmailConfirmed, " +
                                        "PasswordHash, " +
                                        "SecurityStamp, " +
                                        "PhoneNumber, " +
                                        "PhoneNumberConfirmed, " +
                                        "TwoFactorEnabled, " +
                                        "LockoutEnabled, " + 
                                        "AccessFailedCount, " +
                                        "Address, " +
                                        "City, " +
                                        "Country, " +
                                        "FirstName, " +
                                        "LastName, " +
                                        "PostalCode) ");
            sb.AppendLine("VALUES(");
            sb.AppendLine($"'{ADMIN_USER_GUID}',");
            sb.AppendLine($"'admin@admin.com',");
            sb.AppendLine($"'ADMIN@ADMIN.COM',");
            sb.AppendLine($"'admin@admin.com',");
            sb.AppendLine($"'ADMIN@ADMIN.COM',");
            sb.AppendLine($"0,");
            sb.AppendLine($"'{passwordhash}',");
            sb.AppendLine($"'Admin',");
            sb.AppendLine($"'+385912122512',");
            sb.AppendLine($"1,");
            sb.AppendLine($"0,");
            sb.AppendLine($"0,");
            sb.AppendLine($"0,");
            sb.AppendLine($"'Licka 35',");
            sb.AppendLine($"'Zagreb',");
            sb.AppendLine($"'Croatia',");
            sb.AppendLine($"'Stjepan',");
            sb.AppendLine($"'Mateljan',");
            sb.AppendLine($"'10000'");
            sb.AppendLine(")");

            migrationBuilder.Sql(sb.ToString());

            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{ADMIN_ROLE_GUID}', 'Admin', 'ADMIN')");

            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{ADMIN_USER_GUID}','{ADMIN_ROLE_GUID}')");
        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId='{ADMIN_USER_GUID}' AND RoleId='{ADMIN_ROLE_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id='{ADMIN_ROLE_GUID}'");

            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id='{ADMIN_USER_GUID}'");
        }
    }
}
