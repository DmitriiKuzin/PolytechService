using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCore2.Migrations
{
    public partial class dataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "2ecccbe7-3859-40c7-bbb7-81eabe1e55e1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ecccbe7-3859-40c7-bbb7-81eabe1e55e1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "05688859-8193-41ed-9764-5d1a6312f6c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "9416c912-c25c-4762-90fa-5bb0de05b0be");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "DormId", "FullName", "Room" },
                values: new object[] { "f695e0db-46cd-4329-8614-fe77604eee27", 0, "f4fddcb9-797e-4e24-bd1a-6e40c610b494", "AppUser", null, true, false, null, null, null, "AQAAAAEAACcQAAAAELo1fYzlheFYaVUvvr6bo0jESyvK8DV6Ge+qljX4lzspxIf5tTnR3hdUIvhHuOyuRA==", null, false, null, false, "Admin", null, null, null });

            migrationBuilder.InsertData(
                table: "Dorm",
                columns: new[] { "Id", "Address" },
                values: new object[] { 1, "ул. Примерная д28" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId", "Discriminator" },
                values: new object[] { "f695e0db-46cd-4329-8614-fe77604eee27", "1", "UserRoles" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "f695e0db-46cd-4329-8614-fe77604eee27", "1" });

            migrationBuilder.DeleteData(
                table: "Dorm",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f695e0db-46cd-4329-8614-fe77604eee27");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "6525ad65-81da-49cc-99b7-d887230c880e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "6001ee66-dfc1-4e8b-bd47-ed977a42cc24");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "DormId", "FullName", "Room" },
                values: new object[] { "2ecccbe7-3859-40c7-bbb7-81eabe1e55e1", 0, "74936ed1-8a85-4a44-aa42-50ba9fab4041", "AppUser", null, true, false, null, null, null, "AQAAAAEAACcQAAAAEPrpccDLM7DDkDXqmkEDVw6kt7URG08wEQzk5jV3wJyk3YWdOE5+BZZjL/+ahT++LQ==", null, false, null, false, "Admin", null, null, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId", "Discriminator" },
                values: new object[] { "2ecccbe7-3859-40c7-bbb7-81eabe1e55e1", "1", "UserRoles" });
        }
    }
}
