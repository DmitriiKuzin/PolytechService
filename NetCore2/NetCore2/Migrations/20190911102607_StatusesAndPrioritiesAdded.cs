using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCore2.Migrations
{
    public partial class StatusesAndPrioritiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "91f896dd-5b85-42c5-a12a-99fb3980da59", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91f896dd-5b85-42c5-a12a-99fb3980da59");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "d3250845-f7a7-4ffe-8229-438820e2630f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "deb06113-042a-440b-808c-97d9d4a3618f");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "DormId", "FullName", "Room" },
                values: new object[] { "280a0f84-99ef-4c2a-9485-cd461889f926", 0, "0193e5c5-2b52-4c7f-9e2b-61b88d967408", "AppUser", null, true, false, null, null, null, "AQAAAAEAACcQAAAAEBnDyV9va2zbUkUU9roTfq5v1aXd0qzldZkhqShM/GbbbPpQJBazzNfEaPV9FpjmlA==", null, false, null, false, "Admin", null, null, null });

            migrationBuilder.InsertData(
                table: "Priority",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Высокий" },
                    { 2, "Средний" },
                    { 3, "Низкий" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Открыта" },
                    { 2, "Закрыта" },
                    { 3, "Закрыта неуспешно" },
                    { 4, "Пауза" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId", "Discriminator" },
                values: new object[] { "280a0f84-99ef-4c2a-9485-cd461889f926", "1", "UserRoles" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "280a0f84-99ef-4c2a-9485-cd461889f926", "1" });

            migrationBuilder.DeleteData(
                table: "Priority",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Priority",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Priority",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280a0f84-99ef-4c2a-9485-cd461889f926");

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
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId", "Discriminator" },
                values: new object[] { "91f896dd-5b85-42c5-a12a-99fb3980da59", "1", "UserRoles" });
        }
    }
}
