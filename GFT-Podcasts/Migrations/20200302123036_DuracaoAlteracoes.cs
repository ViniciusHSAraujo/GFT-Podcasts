using Microsoft.EntityFrameworkCore.Migrations;

namespace GFT_Podcasts.Migrations
{
    public partial class DuracaoAlteracoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Duracao",
                table: "Episodios",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Duracao",
                table: "Episodios",
                type: "double",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
