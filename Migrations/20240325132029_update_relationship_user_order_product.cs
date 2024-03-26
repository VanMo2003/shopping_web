using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace website_shopping.Migrations
{
    /// <inheritdoc />
    public partial class update_relationship_user_order_product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "list_product",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "cart",
                table: "Users",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrderModelId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "full_name",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderModelId",
                table: "Products",
                column: "OrderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_full_name",
                table: "Orders",
                column: "full_name");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_full_name",
                table: "Orders",
                column: "full_name",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderModelId",
                table: "Products",
                column: "OrderModelId",
                principalTable: "Orders",
                principalColumn: "id_order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_full_name",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderModelId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderModelId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_full_name",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "cart",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OrderModelId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "full_name",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "list_product",
                table: "Orders",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }
    }
}
