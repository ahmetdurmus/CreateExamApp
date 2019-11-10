using Microsoft.EntityFrameworkCore.Migrations;

namespace CreateExamApp.Data.Migrations
{
    public partial class AddTitleField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "exams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "title",
                table: "exams");
        }
    }
}
