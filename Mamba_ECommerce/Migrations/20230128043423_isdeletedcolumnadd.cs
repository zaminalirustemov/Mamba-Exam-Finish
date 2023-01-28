using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mamba_ECommerce.Migrations
{
    public partial class isdeletedcolumnadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Teams",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Teams");
        }
    }
}
