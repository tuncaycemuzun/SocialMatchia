using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMatchia.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class socialMediadAddedOrderColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "SocialMedias",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "SocialMedias");
        }
    }
}
