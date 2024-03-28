using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMatchia.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changedSocialMediaSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "SocialMedias");

            migrationBuilder.RenameColumn(
                name: "IconPath",
                table: "SocialMedias",
                newName: "IconName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IconName",
                table: "SocialMedias",
                newName: "IconPath");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "SocialMedias",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "SocialMedias",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "SocialMedias",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "SocialMedias",
                type: "uuid",
                nullable: true);
        }
    }
}
