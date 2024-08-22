using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMatchia.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class userInformationAddedBirthDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "UserInformations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "UserInformations");
        }
    }
}
