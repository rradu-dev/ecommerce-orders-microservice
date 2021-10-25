using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Services.Orders.Infrastructure.Database.Migrations
{
    public partial class CartIdColumnUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_order_CartId",
                table: "order",
                column: "CartId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_order_CartId",
                table: "order");
        }
    }
}
