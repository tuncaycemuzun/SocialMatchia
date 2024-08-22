using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMatchia.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removeColumnsInUserInformatio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInformations_Countries_CountryId",
                table: "UserInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInformations_Towns_TownId",
                table: "UserInformations");

            migrationBuilder.DropIndex(
                name: "IX_UserInformations_CountryId",
                table: "UserInformations");

            migrationBuilder.DropIndex(
                name: "IX_UserInformations_TownId",
                table: "UserInformations");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "UserInformations");

            migrationBuilder.DropColumn(
                name: "TownId",
                table: "UserInformations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "UserInformations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TownId",
                table: "UserInformations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserInformations_CountryId",
                table: "UserInformations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInformations_TownId",
                table: "UserInformations",
                column: "TownId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInformations_Countries_CountryId",
                table: "UserInformations",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInformations_Towns_TownId",
                table: "UserInformations",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
