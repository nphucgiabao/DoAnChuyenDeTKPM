using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace booking_api.Migrations
{
    public partial class UpdateTableBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "Booking",
                type: "decimal",
                nullable: false
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Booking"
                );
        }
    }
}
