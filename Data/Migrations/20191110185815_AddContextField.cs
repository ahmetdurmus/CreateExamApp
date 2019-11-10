using Microsoft.EntityFrameworkCore.Migrations;

namespace CreateExamApp.Data.Migrations
{
    public partial class AddContextField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "context",
                table: "exams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "context",
                table: "exams");
        }
    }
}
