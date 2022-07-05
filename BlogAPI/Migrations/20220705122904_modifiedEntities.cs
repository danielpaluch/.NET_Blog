using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogAPI.Migrations
{
    public partial class modifiedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedByUserId",
                table: "Comments",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CreatedByUserId",
                table: "Blogs",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_CreatedByUserId",
                table: "Blogs",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_CreatedByUserId",
                table: "Comments",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_CreatedByUserId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_CreatedByUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CreatedByUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_CreatedByUserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Blogs");
        }
    }
}
