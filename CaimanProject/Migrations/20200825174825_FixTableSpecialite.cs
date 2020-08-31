using Microsoft.EntityFrameworkCore.Migrations;

namespace CaimanProject.Migrations
{
    public partial class FixTableSpecialite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageSpecialité",
                table: "Specialites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url_Image",
                table: "Specialites",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSpecialité",
                table: "Specialites");

            migrationBuilder.DropColumn(
                name: "Url_Image",
                table: "Specialites");
        }
    }
}
