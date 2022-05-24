using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Entities.Migrations
{
    public partial class ActionDto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Organizer",
                table: "RolesDto",
                newName: "IsOrganizer");

            migrationBuilder.AddColumn<int>(
                name: "ActionId",
                table: "Teams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionId",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActionName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BeginDate", "BirthDate", "ConcurrencyStamp", "DepartmentCode", "DepartmentId", "DepartmentName", "Email", "EmailConfirmed", "EndDate", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PersonalNumber", "PhoneNumber", "PhoneNumberConfirmed", "PlaceCode", "PlaceId", "PlaceName", "PositionCode", "PositionName", "PwdSalt", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "93ae2a79-4ae8-4ba5-b5bd-7ab6f4422b32", 0, new DateTime(2020, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1970, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9319d1ea-edd6-44e7-b0f7-ee859a92b74b", "0035", 4, null, "Vasya0Pupka@mail.ru", false, new DateTime(2022, 5, 24, 19, 2, 36, 609, DateTimeKind.Local).AddTicks(1079), "Vasya", "Pupkin", false, null, "Vitlievich", null, null, "123456", 1, "664363", null, false, null, 1, null, null, null, "sal", "4d3db46a-4ed0-4e55-b648-c089a4b05aed", false, "nagibator228" },
                    { "69236048-8df4-4afa-bbb3-43a6b5b4c2b4", 0, new DateTime(2012, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1960, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "72e3c978-1e18-42c9-b3f6-aaa43629b0b0", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 24, 19, 2, 36, 609, DateTimeKind.Local).AddTicks(8367), "Volodya", "Putin", false, null, "Vladimirivich", null, null, "ukrainIsMine", 2, "44325", null, false, null, 0, null, null, null, "gg", "19293778-bfeb-4757-9a75-fd1f3d814e46", false, "VZPutin" },
                    { "50a9e30a-fd52-4588-9422-9ae5abe5210e", 0, new DateTime(2017, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cbf7f719-8566-4c0a-8169-6470e41e4075", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 24, 19, 2, 36, 609, DateTimeKind.Local).AddTicks(8390), "Vlad", "Vladov", false, null, "Vladimirivich", null, null, "12345", 3, "1999", null, false, null, 0, null, null, null, "hh", "927e1ee7-b5f3-4bdf-83ea-02a13b8f62f8", false, "Killer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ActionId",
                table: "Teams",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ActionId",
                table: "Participants",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_TeamId",
                table: "Participants",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Actions_ActionId",
                table: "Participants",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Teams_TeamId",
                table: "Participants",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Actions_ActionId",
                table: "Teams",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Actions_ActionId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Teams_TeamId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Actions_ActionId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ActionId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ActionId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_TeamId",
                table: "Participants");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "50a9e30a-fd52-4588-9422-9ae5abe5210e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69236048-8df4-4afa-bbb3-43a6b5b4c2b4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93ae2a79-4ae8-4ba5-b5bd-7ab6f4422b32");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "IsOrganizer",
                table: "RolesDto",
                newName: "Organizer");

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
    }
}
