using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCore2.Migrations
{
    public partial class categoriesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "f695e0db-46cd-4329-8614-fe77604eee27", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f695e0db-46cd-4329-8614-fe77604eee27");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "525742f2-f4db-4631-a205-634726490185");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "e5824596-ab5d-43db-830b-1201e371a5ad");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "DormId", "FullName", "Room" },
                values: new object[] { "91f896dd-5b85-42c5-a12a-99fb3980da59", 0, "44d602bd-ea7e-4e75-9e4a-3dd54d9d104c", "AppUser", null, true, false, null, null, null, "AQAAAAEAACcQAAAAEO1HWwzXsy2+SeHpMeh9Ocm4LXk0Ja2xBEZkTTQHpYgfKnpllyIx6sAea5S/Dskr7Q==", null, false, null, false, "Admin", null, null, null });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Сантехника" },
                    { 2, "Электрика" },
                    { 3, "Мебель" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId", "Discriminator" },
                values: new object[] { "91f896dd-5b85-42c5-a12a-99fb3980da59", "1", "UserRoles" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "91f896dd-5b85-42c5-a12a-99fb3980da59", "1" });

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91f896dd-5b85-42c5-a12a-99fb3980da59");

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
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId", "Discriminator" },
                values: new object[] { "f695e0db-46cd-4329-8614-fe77604eee27", "1", "UserRoles" });
        }
    }
}
