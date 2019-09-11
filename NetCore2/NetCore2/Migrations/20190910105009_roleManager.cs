using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCore2.Migrations
{
    public partial class roleManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f73a84b4-d10f-47b6-9ac6-258d68e5b184");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "rusName" },
                values: new object[] { "1", "6525ad65-81da-49cc-99b7-d887230c880e", "IdentRole", "Admin", "ADMIN", "Администратор" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "rusName" },
                values: new object[] { "2", "6001ee66-dfc1-4e8b-bd47-ed977a42cc24", "IdentRole", "User", "USER", "Пользователь" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "DormId", "FullName", "Room" },
                values: new object[] { "2ecccbe7-3859-40c7-bbb7-81eabe1e55e1", 0, "74936ed1-8a85-4a44-aa42-50ba9fab4041", "AppUser", null, true, false, null, null, null, "AQAAAAEAACcQAAAAEPrpccDLM7DDkDXqmkEDVw6kt7URG08wEQzk5jV3wJyk3YWdOE5+BZZjL/+ahT++LQ==", null, false, null, false, "Admin", null, null, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId", "Discriminator" },
                values: new object[] { "2ecccbe7-3859-40c7-bbb7-81eabe1e55e1", "1", "UserRoles" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "2ecccbe7-3859-40c7-bbb7-81eabe1e55e1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ecccbe7-3859-40c7-bbb7-81eabe1e55e1");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "DormId", "FullName", "Room" },
                values: new object[] { "f73a84b4-d10f-47b6-9ac6-258d68e5b184", 0, "7e635610-5fd9-4b40-95b8-432c61679a0b", "AppUser", null, true, false, null, null, null, "AQAAAAEAACcQAAAAEKCSgw6CG34zkfoUZKIf1F1jU3OOR/LhiNUtGOzevn5RfbP22GaXEVMexSOOKLDa7A==", null, false, null, false, "Admin", null, null, null });
        }
    }
}
