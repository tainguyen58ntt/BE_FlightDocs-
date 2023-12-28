using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocs.Service.DocumentApi.Migrations
{
    public partial class addNoteFieldToDocumentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "DocumentType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "DocumentType");
        }
    }
}
