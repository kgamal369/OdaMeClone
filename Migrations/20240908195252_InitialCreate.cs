using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdaMeClone.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddOns_Apartments_ApartmentId1",
                schema: "OdaMeClone",
                table: "AddOns");

            migrationBuilder.DropIndex(
                name: "IX_AddOns_ApartmentId1",
                schema: "OdaMeClone",
                table: "AddOns");

            migrationBuilder.DropColumn(
                name: "ApartmentId1",
                schema: "OdaMeClone",
                table: "AddOns");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApartmentId1",
                schema: "OdaMeClone",
                table: "AddOns",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddOns_ApartmentId1",
                schema: "OdaMeClone",
                table: "AddOns",
                column: "ApartmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AddOns_Apartments_ApartmentId1",
                schema: "OdaMeClone",
                table: "AddOns",
                column: "ApartmentId1",
                principalSchema: "OdaMeClone",
                principalTable: "Apartments",
                principalColumn: "ApartmentId");
        }
    }
}
