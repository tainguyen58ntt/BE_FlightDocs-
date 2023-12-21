using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocs.Service.DocumentApi.Migrations
{
    public partial class AddCreateByToGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Groups");
        }
    }
}
