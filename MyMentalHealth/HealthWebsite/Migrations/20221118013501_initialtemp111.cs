using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMentalHealth.Migrations
{
    public partial class initialtemp111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exercise_VideoLink",
                table: "Contents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Exercise_VideoLink",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
