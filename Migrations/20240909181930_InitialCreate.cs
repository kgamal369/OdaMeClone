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
                name: "FK_AddOns_Apartments_ApartmentId",
                schema: "OdaMeClone",
                table: "AddOns");

            migrationBuilder.DropIndex(
                name: "IX_AddOns_ApartmentId",
                schema: "OdaMeClone",
                table: "AddOns");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                schema: "OdaMeClone",
                table: "AddOns");

            migrationBuilder.AddColumn<Guid>(
                name: "ApartmentId1",
                schema: "OdaMeClone",
                table: "ApartmentAddOns",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentAddOns_ApartmentId1",
                schema: "OdaMeClone",
                table: "ApartmentAddOns",
                column: "ApartmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentAddOns_Apartments_ApartmentId1",
                schema: "OdaMeClone",
                table: "ApartmentAddOns",
                column: "ApartmentId1",
                principalSchema: "OdaMeClone",
                principalTable: "Apartments",
                principalColumn: "ApartmentId");
            }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
            {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentAddOns_Apartments_ApartmentId1",
                schema: "OdaMeClone",
                table: "ApartmentAddOns");

            migrationBuilder.DropIndex(
                name: "IX_ApartmentAddOns_ApartmentId1",
                schema: "OdaMeClone",
                table: "ApartmentAddOns");

            migrationBuilder.DropColumn(
                name: "ApartmentId1",
                schema: "OdaMeClone",
                table: "ApartmentAddOns");

            migrationBuilder.AddColumn<Guid>(
                name: "ApartmentId",
                schema: "OdaMeClone",
                table: "AddOns",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddOns_ApartmentId",
                schema: "OdaMeClone",
                table: "AddOns",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddOns_Apartments_ApartmentId",
                schema: "OdaMeClone",
                table: "AddOns",
                column: "ApartmentId",
                principalSchema: "OdaMeClone",
                principalTable: "Apartments",
                principalColumn: "ApartmentId");
            }
        }
    }
