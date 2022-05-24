using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Entities.Migrations
{
    public partial class ApplicationUserOneToOne : Migration
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
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserForeignKey",
                table: "Participants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ActionDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActionName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionDto", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BeginDate", "BirthDate", "ConcurrencyStamp", "DepartmentCode", "DepartmentId", "DepartmentName", "Email", "EmailConfirmed", "EndDate", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PersonalNumber", "PhoneNumber", "PhoneNumberConfirmed", "PlaceCode", "PlaceId", "PlaceName", "PositionCode", "PositionName", "PwdSalt", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "97b62bcf-2076-4f69-b1ef-812d825f813a", 0, new DateTime(2020, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1970, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "51d514b6-d27a-41ef-a77e-8cc009ad23b0", "0035", 4, null, "Vasya0Pupka@mail.ru", false, new DateTime(2022, 5, 24, 20, 48, 54, 204, DateTimeKind.Local).AddTicks(5248), "Vasya", "Pupkin", false, null, "Vitlievich", null, null, "123456", 1, "664363", null, false, null, 1, null, null, null, "sal", "f11e416a-1280-4019-9bd6-5ed04f3f5a07", false, "nagibator228" },
                    { "c0e4c637-bbed-4957-9a2f-3dab761c8127", 0, new DateTime(2012, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1960, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "6db5295a-f5d7-46ab-8b35-fbbbcb3bc5f3", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 24, 20, 48, 54, 205, DateTimeKind.Local).AddTicks(4764), "Volodya", "Putin", false, null, "Vladimirivich", null, null, "ukrainIsMine", 2, "44325", null, false, null, 0, null, null, null, "gg", "3879d95e-31ce-47f2-bb01-a44008de5815", false, "VZPutin" },
                    { "a3e69134-b680-4629-a31a-31e0b0ee2136", 0, new DateTime(2017, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "25b6166a-34cb-4ec8-a399-5559a367054e", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 24, 20, 48, 54, 205, DateTimeKind.Local).AddTicks(4790), "Vlad", "Vladov", false, null, "Vladimirivich", null, null, "12345", 3, "1999", null, false, null, 0, null, null, null, "hh", "6c408136-d29f-4109-b777-988d49ee2276", false, "Killer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ActionId",
                table: "Participants",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ApplicationUserForeignKey",
                table: "Participants",
                column: "ApplicationUserForeignKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_TeamId",
                table: "Participants",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_ActionDto_ActionId",
                table: "Participants",
                column: "ActionId",
                principalTable: "ActionDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_AspNetUsers_ApplicationUserForeignKey",
                table: "Participants",
                column: "ApplicationUserForeignKey",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Teams_TeamId",
                table: "Participants",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_ActionDto_ActionId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_AspNetUsers_ApplicationUserForeignKey",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Teams_TeamId",
                table: "Participants");

            migrationBuilder.DropTable(
                name: "ActionDto");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ActionId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ApplicationUserForeignKey",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_TeamId",
                table: "Participants");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "97b62bcf-2076-4f69-b1ef-812d825f813a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a3e69134-b680-4629-a31a-31e0b0ee2136");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c0e4c637-bbed-4957-9a2f-3dab761c8127");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "ApplicationUserForeignKey",
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
