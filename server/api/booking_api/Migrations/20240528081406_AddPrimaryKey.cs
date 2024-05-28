using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace booking_api.Migrations
{
    public partial class AddPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
            name: "Id",
            table: "Driver",
            type: "uniqueidentifier"
        );
            migrationBuilder.AddPrimaryKey(
                name: "PK_Driver",
                table: "Driver",
                column: "Id");
            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingHistory",
                table: "BookingHistory",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(name: "PK_Driver", table: "Driver");
            migrationBuilder.DropPrimaryKey(name: "PK_BookingHistory", table: "BookingHistory");
        }
    }
}
