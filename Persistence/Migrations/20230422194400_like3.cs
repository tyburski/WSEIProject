using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class like3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CommentId",
                table: "Users",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Comments_CommentId",
                table: "Users",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Comments_CommentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CommentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Users");
        }
    }
}
