using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMatchia.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class tableSchemaEditSocialMedias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserSocialMedias",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserSocialMedias");
        }
    }
}
