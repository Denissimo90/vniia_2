using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Entities.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PersonalNumber = table.Column<string>(type: "text", nullable: true),
                    PositionCode = table.Column<string>(type: "text", nullable: true),
                    PositionName = table.Column<string>(type: "text", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    DepartmentCode = table.Column<string>(type: "text", nullable: true),
                    DepartmentName = table.Column<string>(type: "text", nullable: true),
                    PlaceId = table.Column<int>(type: "integer", nullable: false),
                    PlaceCode = table.Column<string>(type: "text", nullable: true),
                    PlaceName = table.Column<string>(type: "text", nullable: true),
                    BeginDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PwdSalt = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufactures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Caption = table.Column<string>(type: "text", nullable: true),
                    Entered = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufactures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Articul = table.Column<string>(type: "text", nullable: true),
                    Caption = table.Column<string>(type: "text", nullable: true),
                    Measure = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ManufactureId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Manufactures_ManufactureId",
                        column: x => x.ManufactureId,
                        principalTable: "Manufactures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductQties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    DateOfManufacture = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductQties_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AccessFailedCount", "BeginDate", "BirthDate", "ConcurrencyStamp", "DepartmentCode", "DepartmentId", "DepartmentName", "Email", "EmailConfirmed", "EndDate", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PersonalNumber", "PhoneNumber", "PhoneNumberConfirmed", "PlaceCode", "PlaceId", "PlaceName", "PositionCode", "PositionName", "PwdSalt", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2020, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1970, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "f69acad0-b97c-4b75-a343-a67deb0393c3", "0035", 4, null, "Vasya0Pupka@mail.ru", false, new DateTime(2022, 5, 23, 15, 40, 48, 279, DateTimeKind.Local).AddTicks(5502), "Vasya", "Pupkin", false, null, "Vitlievich", null, null, "123456", 1, "664363", null, false, null, 1, null, null, null, "sal", "93669b1f-9379-4ba0-a261-b2ee0038d67d", false, "nagibator228" },
                    { 2, 0, new DateTime(2012, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1960, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "47cd1e19-ad67-45f9-b4e9-33f677e0af91", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 23, 15, 40, 48, 280, DateTimeKind.Local).AddTicks(6909), "Volodya", "Putin", false, null, "Vladimirivich", null, null, "ukrainIsMine", 2, "44325", null, false, null, 0, null, null, null, "gg", "06075495-6d98-412a-9b4f-dd83aa833c35", false, "VZPutin" },
                    { 3, 0, new DateTime(2017, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1980, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "140d176f-8235-451b-9257-5cf3e0d7f1e8", null, 0, null, "killer@gmail.com", false, new DateTime(2022, 5, 23, 15, 40, 48, 280, DateTimeKind.Local).AddTicks(6940), "Vlad", "Vladov", false, null, "Vladimirivich", null, null, "12345", 3, "1999", null, false, null, 0, null, null, null, "hh", "f3e5729e-2484-4f99-917b-933dac24e211", false, "Killer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductQties_ProductId",
                table: "ProductQties",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufactureId",
                table: "Products",
                column: "ManufactureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "ProductQties");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Manufactures");
        }
    }
}
