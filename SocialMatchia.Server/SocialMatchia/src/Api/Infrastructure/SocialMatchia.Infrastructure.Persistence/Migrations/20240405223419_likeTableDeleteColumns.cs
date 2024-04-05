using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMatchia.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class likeTableDeleteColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_SourceUserId1",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_TargetUserId1",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_SourceUserId1",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_TargetUserId1",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "SourceUserId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "SourceUserId1",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "TargetUserId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "TargetUserId1",
                table: "Likes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SourceUserId",
                table: "Likes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceUserId1",
                table: "Likes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "TargetUserId",
                table: "Likes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "TargetUserId1",
                table: "Likes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Likes_SourceUserId1",
                table: "Likes",
                column: "SourceUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_TargetUserId1",
                table: "Likes",
                column: "TargetUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_SourceUserId1",
                table: "Likes",
                column: "SourceUserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_TargetUserId1",
                table: "Likes",
                column: "TargetUserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
