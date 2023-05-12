using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPDV.WApp.Migrations.OrderDb
{
    /// <inheritdoc />
    public partial class AddedOrderProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "SubTotal",
                table: "OrderProducts",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderProducts",
                newName: "SubTotal");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
