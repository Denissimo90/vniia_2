using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Entities.Migrations
{
    public partial class IdUserValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "04ebf8b0-f8d9-4971-91e9-7ac8de67041a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d4d765a-ef54-47e8-a99f-9ab8843fd615");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ac542394-ea1a-434f-a2de-7a2c584ecb59");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BeginDate", "BirthDate", "ConcurrencyStamp", "DepartmentCode", "DepartmentId", "DepartmentName", "Email", "EmailConfirmed", "EndDate", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PersonalNumber", "PhoneNumber", "PhoneNumberConfirmed", "PlaceCode", "PlaceId", "PlaceName", "PositionCode", "PositionName", "PwdSalt", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "eb8638e6-cc1e-447e-8d10-4d4aea334153", 0, new DateTime(2020, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1970, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "6d04bebb-7463-436d-b97e-6430ac202e33", "0035", 4, null, "Vasya0Pupka@mail.ru", false, new DateTime(2022, 5, 24, 17, 3, 6, 60, DateTimeKind.Local).AddTicks(295), "Vasya", "Pupkin", false, null, "Vitlievich", null, null, "123456", 1, "664363", null, false, null, 1, null, null, null, "sal", "f4ac0503-a80d-4e0c-a832-36395d0ba00f", false, "nagibator228" },
                    { "da657fae-36df-479c-b901-26ccc636894f", 0, new DateTime(2012, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1960, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ed84869e-6e87-44a3-9c13-7e6575e8b251", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 24, 17, 3, 6, 60, DateTimeKind.Local).AddTicks(7727), "Volodya", "Putin", false, null, "Vladimirivich", null, null, "ukrainIsMine", 2, "44325", null, false, null, 0, null, null, null, "gg", "5fd49d93-fb15-437e-9aec-e02eae015285", false, "VZPutin" },
                    { "23b8ac02-b92a-4d97-b49d-7a153895dcfb", 0, new DateTime(2017, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "73ac8892-0cbc-4930-bfe0-0b1ed98d57b2", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 24, 17, 3, 6, 60, DateTimeKind.Local).AddTicks(7750), "Vlad", "Vladov", false, null, "Vladimirivich", null, null, "12345", 3, "1999", null, false, null, 0, null, null, null, "hh", "06652f15-0de5-4113-a48b-5da99ef90a82", false, "Killer" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "23b8ac02-b92a-4d97-b49d-7a153895dcfb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da657fae-36df-479c-b901-26ccc636894f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb8638e6-cc1e-447e-8d10-4d4aea334153");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BeginDate", "BirthDate", "ConcurrencyStamp", "DepartmentCode", "DepartmentId", "DepartmentName", "Email", "EmailConfirmed", "EndDate", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PersonalNumber", "PhoneNumber", "PhoneNumberConfirmed", "PlaceCode", "PlaceId", "PlaceName", "PositionCode", "PositionName", "PwdSalt", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5d4d765a-ef54-47e8-a99f-9ab8843fd615", 0, new DateTime(2020, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1970, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b5e513a6-0fdb-4313-ae4a-768e7bcc65ea", "0035", 4, null, "Vasya0Pupka@mail.ru", false, new DateTime(2022, 5, 24, 10, 45, 37, 892, DateTimeKind.Local).AddTicks(365), "Vasya", "Pupkin", false, null, "Vitlievich", null, null, "123456", 1, "664363", null, false, null, 1, null, null, null, "sal", "aa1d07db-b5c1-4809-8c76-68f9f4aa84f5", false, "nagibator228" },
                    { "ac542394-ea1a-434f-a2de-7a2c584ecb59", 0, new DateTime(2012, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1960, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b5cc3223-4a44-4cb3-b45d-62b92c17d126", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 24, 10, 45, 37, 892, DateTimeKind.Local).AddTicks(7921), "Volodya", "Putin", false, null, "Vladimirivich", null, null, "ukrainIsMine", 2, "44325", null, false, null, 0, null, null, null, "gg", "ba04f5c4-3de7-4f33-8e72-d2f056e794e1", false, "VZPutin" },
                    { "04ebf8b0-f8d9-4971-91e9-7ac8de67041a", 0, new DateTime(2017, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b7cb239c-c497-4e80-86f9-77bff9e13f6d", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 24, 10, 45, 37, 892, DateTimeKind.Local).AddTicks(7941), "Vlad", "Vladov", false, null, "Vladimirivich", null, null, "12345", 3, "1999", null, false, null, 0, null, null, null, "hh", "e9ed185f-6bf7-488c-951c-97e6fd1c6127", false, "Killer" }
                });
        }
    }
}
