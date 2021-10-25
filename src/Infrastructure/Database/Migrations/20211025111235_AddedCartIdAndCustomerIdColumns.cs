using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Services.Orders.Infrastructure.Database.Migrations
{
    public partial class AddedCartIdAndCustomerIdColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CartId",
                table: "order",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "customer",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartId",
                table: "order");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "customer");
        }
    }
}
