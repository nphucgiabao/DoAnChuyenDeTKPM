using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace user_api.Migrations
{
    public partial class UpdateTableAspNetUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
               name: "OId",
               table: "AspNetUsers",
               nullable: true,
               type: "uniqueidentifier");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OId",
                table: "AspNetUsers");
        }
    }
}
