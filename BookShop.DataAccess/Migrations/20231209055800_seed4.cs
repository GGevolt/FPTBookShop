using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTBookShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seed4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Request_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Request_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Request_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ApplicationUserId", "UserId" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "ApplicationUserId", "UserId" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "ApplicationUserId", "UserId" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "ApplicationUserId", "UserId" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "ApplicationUserId", "UserId" },
                values: new object[] { null, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ApplicationUserId",
                table: "Categories",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_ApplicationUserId",
                table: "Categories",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_ApplicationUserId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ApplicationUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Categories");
        }
    }
}
