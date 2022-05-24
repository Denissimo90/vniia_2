using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Entities.Migrations
{
    public partial class mig_workly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_ActionDto_ActionId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_AspNetUsers_ApplicationUserForeignKey",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Competents_CompetentionId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_RolesDto_RoleId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Teams_TeamId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Competents_CompetentionId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "ActionDto");

            migrationBuilder.DropTable(
                name: "RolesDto");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ActionId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ApplicationUserForeignKey",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_CompetentionId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_RoleId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_TeamId",
                table: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competents",
                table: "Competents");

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
                name: "CompetentionId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "ThirdName",
                table: "Participants");

            migrationBuilder.RenameTable(
                name: "Competents",
                newName: "CompetentionDto");

            migrationBuilder.AlterColumn<int>(
                name: "CompetentionId",
                table: "Teams",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "TeamDtoId",
                table: "Teams",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompetentionId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipantDtoId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleApiDtoId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleDtoId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetentionDto",
                table: "CompetentionDto",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Compitentions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    ShortTitle = table.Column<string>(type: "text", nullable: true),
                    CompetentionDtoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compitentions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compitentions_CompetentionDto_CompetentionDtoId",
                        column: x => x.CompetentionDtoId,
                        principalTable: "CompetentionDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsOrganizer = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CompetentionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamDto_CompetentionDto_CompetentionId",
                        column: x => x.CompetentionId,
                        principalTable: "CompetentionDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    SecondName = table.Column<string>(type: "text", nullable: true),
                    ThirdName = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    CompetentionId = table.Column<int>(type: "integer", nullable: true),
                    CompetentionDtoId = table.Column<int>(type: "integer", nullable: true),
                    TeamDtoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantDto_CompetentionDto_CompetentionDtoId",
                        column: x => x.CompetentionDtoId,
                        principalTable: "CompetentionDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParticipantDto_RoleDto_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantDto_TeamDto_TeamDtoId",
                        column: x => x.TeamDtoId,
                        principalTable: "TeamDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BeginDate", "BirthDate", "CompetentionId", "ConcurrencyStamp", "DepartmentCode", "DepartmentId", "DepartmentName", "Email", "EmailConfirmed", "EndDate", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "ParticipantDtoId", "PasswordHash", "PersonId", "PersonalNumber", "PhoneNumber", "PhoneNumberConfirmed", "PlaceCode", "PlaceId", "PlaceName", "PositionCode", "PositionName", "PwdSalt", "RoleApiDtoId", "RoleDtoId", "SecurityStamp", "TeamId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "303b444b-857c-4c09-9561-384606ac8424", 0, new DateTime(2020, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1970, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "7cbe40ef-4a87-4009-92c9-4d63a9148132", "0035", 4, null, "admin@mail.ru", false, new DateTime(2022, 5, 25, 1, 26, 14, 343, DateTimeKind.Local).AddTicks(6393), "Vasya", "Vasya", false, null, "Vitlievich", null, null, null, "admin", 1, "664363", null, false, null, 1, null, null, null, "sal", null, null, "732fab37-a89d-4bb8-9fcf-810f08f4b7d0", null, false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamDtoId",
                table: "Teams",
                column: "TeamDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompetentionId",
                table: "AspNetUsers",
                column: "CompetentionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParticipantDtoId",
                table: "AspNetUsers",
                column: "ParticipantDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleApiDtoId",
                table: "AspNetUsers",
                column: "RoleApiDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TeamId",
                table: "AspNetUsers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Compitentions_CompetentionDtoId",
                table: "Compitentions",
                column: "CompetentionDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantDto_CompetentionDtoId",
                table: "ParticipantDto",
                column: "CompetentionDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantDto_RoleId",
                table: "ParticipantDto",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantDto_TeamDtoId",
                table: "ParticipantDto",
                column: "TeamDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamDto_CompetentionId",
                table: "TeamDto",
                column: "CompetentionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Compitentions_CompetentionId",
                table: "AspNetUsers",
                column: "CompetentionId",
                principalTable: "Compitentions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ParticipantDto_ParticipantDtoId",
                table: "AspNetUsers",
                column: "ParticipantDtoId",
                principalTable: "ParticipantDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RoleDto_RoleApiDtoId",
                table: "AspNetUsers",
                column: "RoleApiDtoId",
                principalTable: "RoleDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_TeamId",
                table: "AspNetUsers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Compitentions_CompetentionId",
                table: "Teams",
                column: "CompetentionId",
                principalTable: "Compitentions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamDto_TeamDtoId",
                table: "Teams",
                column: "TeamDtoId",
                principalTable: "TeamDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Compitentions_CompetentionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ParticipantDto_ParticipantDtoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RoleDto_RoleApiDtoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_TeamId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Compitentions_CompetentionId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamDto_TeamDtoId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Compitentions");

            migrationBuilder.DropTable(
                name: "ParticipantDto");

            migrationBuilder.DropTable(
                name: "RoleDto");

            migrationBuilder.DropTable(
                name: "TeamDto");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamDtoId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompetentionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ParticipantDtoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleApiDtoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TeamId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetentionDto",
                table: "CompetentionDto");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "303b444b-857c-4c09-9561-384606ac8424");

            migrationBuilder.DropColumn(
                name: "TeamDtoId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CompetentionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ParticipantDtoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoleApiDtoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoleDtoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "CompetentionDto",
                newName: "Competents");

            migrationBuilder.AlterColumn<int>(
                name: "CompetentionId",
                table: "Teams",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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
                name: "CompetentionId",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Participants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "Participants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ThirdName",
                table: "Participants",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competents",
                table: "Competents",
                column: "Id");

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

            migrationBuilder.CreateTable(
                name: "RolesDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsOrganizer = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesDto", x => x.Id);
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
                name: "IX_Participants_CompetentionId",
                table: "Participants",
                column: "CompetentionId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_RoleId",
                table: "Participants",
                column: "RoleId");

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
                name: "FK_Participants_Competents_CompetentionId",
                table: "Participants",
                column: "CompetentionId",
                principalTable: "Competents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_RolesDto_RoleId",
                table: "Participants",
                column: "RoleId",
                principalTable: "RolesDto",
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
                name: "FK_Teams_Competents_CompetentionId",
                table: "Teams",
                column: "CompetentionId",
                principalTable: "Competents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
