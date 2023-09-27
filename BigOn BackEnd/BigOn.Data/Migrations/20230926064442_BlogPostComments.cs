using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigOn_WebUI.Migrations
{
    public partial class BlogPostComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublishedBy",
                table: "BlogPosts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlogPostComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPostComments_BlogPostComments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "BlogPostComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogPostComments_BlogPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostComments_ParentId",
                table: "BlogPostComments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostComments_PostId",
                table: "BlogPostComments",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostComments");

            migrationBuilder.DropColumn(
                name: "PublishedBy",
                table: "BlogPosts");
        }
    }
}
