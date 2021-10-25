using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Services.Orders.Infrastructure.Database.Migrations
{
    public partial class FixedOrderDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_address_order_OrderId",
                table: "address");

            migrationBuilder.AddForeignKey(
                name: "FK_address_order_OrderId",
                table: "address",
                column: "OrderId",
                principalTable: "order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_address_order_OrderId",
                table: "address");

            migrationBuilder.AddForeignKey(
                name: "FK_address_order_OrderId",
                table: "address",
                column: "OrderId",
                principalTable: "order",
                principalColumn: "Id");
        }
    }
}
