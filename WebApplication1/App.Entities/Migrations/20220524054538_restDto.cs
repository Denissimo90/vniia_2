using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Entities.Migrations
{
    public partial class restDto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    ShortTitle = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Organizer = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CompetentionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Competents_CompetentionId",
                        column: x => x.CompetentionId,
                        principalTable: "Competents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    SecondName = table.Column<string>(type: "text", nullable: true),
                    ThirdName = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    CompetentionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_Competents_CompetentionId",
                        column: x => x.CompetentionId,
                        principalTable: "Competents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participants_RolesDto_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RolesDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BeginDate", "BirthDate", "ConcurrencyStamp", "DepartmentCode", "DepartmentId", "DepartmentName", "Email", "EmailConfirmed", "EndDate", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PersonalNumber", "PhoneNumber", "PhoneNumberConfirmed", "PlaceCode", "PlaceId", "PlaceName", "PositionCode", "PositionName", "PwdSalt", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5d4d765a-ef54-47e8-a99f-9ab8843fd615", 0, new DateTime(2020, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1970, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b5e513a6-0fdb-4313-ae4a-768e7bcc65ea", "0035", 4, null, "Vasya0Pupka@mail.ru", false, new DateTime(2022, 5, 24, 10, 45, 37, 892, DateTimeKind.Local).AddTicks(365), "Vasya", "Pupkin", false, null, "Vitlievich", null, null, "123456", 1, "664363", null, false, null, 1, null, null, null, "sal", "aa1d07db-b5c1-4809-8c76-68f9f4aa84f5", false, "nagibator228" },
                    { "ac542394-ea1a-434f-a2de-7a2c584ecb59", 0, new DateTime(2012, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1960, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b5cc3223-4a44-4cb3-b45d-62b92c17d126", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 24, 10, 45, 37, 892, DateTimeKind.Local).AddTicks(7921), "Volodya", "Putin", false, null, "Vladimirivich", null, null, "ukrainIsMine", 2, "44325", null, false, null, 0, null, null, null, "gg", "ba04f5c4-3de7-4f33-8e72-d2f056e794e1", false, "VZPutin" },
                    { "04ebf8b0-f8d9-4971-91e9-7ac8de67041a", 0, new DateTime(2017, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b7cb239c-c497-4e80-86f9-77bff9e13f6d", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 24, 10, 45, 37, 892, DateTimeKind.Local).AddTicks(7941), "Vlad", "Vladov", false, null, "Vladimirivich", null, null, "12345", 3, "1999", null, false, null, 0, null, null, null, "hh", "e9ed185f-6bf7-488c-951c-97e6fd1c6127", false, "Killer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_CompetentionId",
                table: "Participants",
                column: "CompetentionId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_RoleId",
                table: "Participants",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CompetentionId",
                table: "Teams",
                column: "CompetentionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "RolesDto");

            migrationBuilder.DropTable(
                name: "Competents");

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
                    { "d094cada-1c6a-4996-b1b7-1beb871ec1a9", 0, new DateTime(2020, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1970, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "d3cca662-f42e-442b-8cd4-c1a642d67048", "0035", 4, null, "Vasya0Pupka@mail.ru", false, new DateTime(2022, 5, 23, 20, 30, 24, 882, DateTimeKind.Local).AddTicks(3120), "Vasya", "Pupkin", false, null, "Vitlievich", null, null, "123456", 1, "664363", null, false, null, 1, null, null, null, "sal", "4b21eb47-fad0-4c90-91fb-ed2eb30a27a2", false, "nagibator228" },
                    { "be4a1ac5-b6f5-45fb-bf22-84aef01781c4", 0, new DateTime(2012, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1960, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e718353b-3444-45a9-b28b-90e0d75e2bb0", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 23, 20, 30, 24, 883, DateTimeKind.Local).AddTicks(8814), "Volodya", "Putin", false, null, "Vladimirivich", null, null, "ukrainIsMine", 2, "44325", null, false, null, 0, null, null, null, "gg", "7559bade-969e-4c22-8d4b-c93479bf7f57", false, "VZPutin" },
                    { "f1c22bfb-8ae4-4738-ab6e-10f6aaf6e57d", 0, new DateTime(2017, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3c6befc2-e781-4179-947f-e43ab6aa2c4b", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 23, 20, 30, 24, 883, DateTimeKind.Local).AddTicks(8939), "Vlad", "Vladov", false, null, "Vladimirivich", null, null, "12345", 3, "1999", null, false, null, 0, null, null, null, "hh", "6a599ccc-a65a-478b-9fa9-aed7037c3834", false, "Killer" }
                });
        }
    }
}
