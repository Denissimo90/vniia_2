using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Entities.Migrations
{
    public partial class ComponentTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b4545349-a202-4f77-bb03-6a56eba3bb74");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BeginDate", "BirthDate", "CompetentionId", "ConcurrencyStamp", "DepartmentCode", "DepartmentId", "DepartmentName", "Email", "EmailConfirmed", "EndDate", "FirstName", "IsNew", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "ParticipantDtoId", "PasswordHash", "PersonId", "PersonalNumber", "PhoneNumber", "PhoneNumberConfirmed", "PlaceCode", "PlaceId", "PlaceName", "PositionCode", "PositionName", "PwdSalt", "RoleDtoId", "SecurityStamp", "TeamId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "da2a0a07-8efd-4598-8b21-066227fe0652", 0, new DateTime(2020, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1970, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "7d171bd7-1177-41b3-8a89-c04c1a0f402f", "0035", 4, null, "admin@mail.ru", false, new DateTime(2022, 5, 25, 15, 39, 28, 136, DateTimeKind.Local).AddTicks(1609), "Vasya", false, "Vasya", false, null, "Vitlievich", null, null, null, "admin", 1, "664363", null, false, null, 1, null, null, null, "sal", null, "1775b146-4126-49a4-8388-fad079c953c4", null, false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CompetentionId",
                table: "Teams",
                column: "CompetentionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Compitentions_CompetentionId",
                table: "Teams",
                column: "CompetentionId",
                principalTable: "Compitentions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Compitentions_CompetentionId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CompetentionId",
                table: "Teams");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "da2a0a07-8efd-4598-8b21-066227fe0652");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BeginDate", "BirthDate", "CompetentionId", "ConcurrencyStamp", "DepartmentCode", "DepartmentId", "DepartmentName", "Email", "EmailConfirmed", "EndDate", "FirstName", "IsNew", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "ParticipantDtoId", "PasswordHash", "PersonId", "PersonalNumber", "PhoneNumber", "PhoneNumberConfirmed", "PlaceCode", "PlaceId", "PlaceName", "PositionCode", "PositionName", "PwdSalt", "RoleDtoId", "SecurityStamp", "TeamId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b4545349-a202-4f77-bb03-6a56eba3bb74", 0, new DateTime(2020, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1970, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "1a4b5767-6e13-41bf-8266-75696bdaea70", "0035", 4, null, "admin@mail.ru", false, new DateTime(2022, 5, 25, 14, 45, 55, 713, DateTimeKind.Local).AddTicks(668), "Vasya", true, "Vasya", false, null, "Vitlievich", null, null, null, "admin", 1, "664363", null, false, null, 1, null, null, null, "sal", null, "f111a926-1031-4bd4-9de2-16a8af67a073", null, false, "admin" });
        }
    }
}
