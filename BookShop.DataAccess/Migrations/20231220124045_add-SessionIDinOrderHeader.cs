using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTBookShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addSessionIDinOrderHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Books_BookID",
                table: "ShoppingCarts");

            migrationBuilder.AlterColumn<int>(
                name: "BookID",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionID",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Books_BookID",
                table: "ShoppingCarts",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Books_BookID",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "SessionID",
                table: "OrderHeaders");

            migrationBuilder.AlterColumn<int>(
                name: "BookID",
                table: "ShoppingCarts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Books_BookID",
                table: "ShoppingCarts",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "ID");
        }
    }
}
