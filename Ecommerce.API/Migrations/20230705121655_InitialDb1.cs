using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class InitialDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProductItemVariantOptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductItemVariantOptions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ProductItemVariantOptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductItemVariantOptions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ProductItemVariantOptions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProductItemVariantOptions",
                type: "TEXT",
                nullable: true);
        }
    }
}
