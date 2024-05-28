using Microsoft.EntityFrameworkCore.Migrations;

namespace booking_api.Migrations
{
    public partial class UpdateTableDriver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Driver",
                nullable: false,
                type: "nvarchar",
                maxLength: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Driver");
        }
    }
}
