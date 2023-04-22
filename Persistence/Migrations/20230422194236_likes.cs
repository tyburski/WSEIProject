using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class likes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikeId",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhotoId",
                table: "Users",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Photos_PhotoId",
                table: "Users",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Photos_PhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PhotoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LikeId",
                table: "Photos");
        }
    }
}
