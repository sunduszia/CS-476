using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

#nullable disable

namespace MyMentalHealth.Migrations
{
    public partial class table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.Sql(
                "INSERT INTO public AdminModels ('UserName', 'Password') VALUES ('Uzban', '12345');"
            );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
