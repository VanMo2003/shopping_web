using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace website_shopping.Migrations
{
    /// <inheritdoc />
    public partial class init_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id_category = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_category = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    time_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    time_update = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id_category);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    username = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    password = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    address = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id_product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_product = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    description_product = table.Column<string>(type: "ntext", maxLength: 1000, nullable: false),
                    unit_price = table.Column<decimal>(type: "money", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    id_category = table.Column<int>(type: "int", nullable: true),
                    time_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    time_update = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id_product);
                    table.ForeignKey(
                        name: "FK_Products_Categories_id_category",
                        column: x => x.id_category,
                        principalTable: "Categories",
                        principalColumn: "id_category");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id_order = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(155)", nullable: false),
                    payment_method = table.Column<bool>(type: "bit", nullable: false),
                    address = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    total_money = table.Column<decimal>(type: "money", nullable: false),
                    list_product = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    time_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    time_update = table.Column<DateTime>(type: "datetime2", nullable: false),
                    payment_status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id_order);
                    table.ForeignKey(
                        name: "FK_Orders_Users_username",
                        column: x => x.username,
                        principalTable: "Users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_username",
                table: "Orders",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_Products_id_category",
                table: "Products",
                column: "id_category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
