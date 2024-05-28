using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace user_api.Migrations
{
    public partial class UpdateTableAspNetUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
               name: "OId",
               table: "AspNetUsers",
               nullable: true,
               type: "varchar",
               maxLength: 100);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OId",
                table: "AspNetUsers");
        }
    }
}
