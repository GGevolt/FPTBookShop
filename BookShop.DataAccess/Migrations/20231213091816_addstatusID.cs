using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTBookShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addstatusID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "OrderStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "Romance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "OrderStatus");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "Romansss");
        }
    }
}
