using Microsoft.EntityFrameworkCore.Migrations;

namespace CreateExamApp.Data.Migrations
{
    public partial class CreateExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    question1 = table.Column<string>(nullable: true),
                    q1a = table.Column<string>(nullable: true),
                    q1b = table.Column<string>(nullable: true),
                    q1c = table.Column<string>(nullable: true),
                    q1d = table.Column<string>(nullable: true),
                    q1Option = table.Column<string>(nullable: true),
                    question2 = table.Column<string>(nullable: true),
                    q2a = table.Column<string>(nullable: true),
                    q2b = table.Column<string>(nullable: true),
                    q2c = table.Column<string>(nullable: true),
                    q2d = table.Column<string>(nullable: true),
                    q2Option = table.Column<string>(nullable: true),
                    question3 = table.Column<string>(nullable: true),
                    q3a = table.Column<string>(nullable: true),
                    q3b = table.Column<string>(nullable: true),
                    q3c = table.Column<string>(nullable: true),
                    q3d = table.Column<string>(nullable: true),
                    q3Option = table.Column<string>(nullable: true),
                    question4 = table.Column<string>(nullable: true),
                    q4a = table.Column<string>(nullable: true),
                    q4b = table.Column<string>(nullable: true),
                    q4c = table.Column<string>(nullable: true),
                    q4d = table.Column<string>(nullable: true),
                    q4Option = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exams", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exams");
        }
    }
}
