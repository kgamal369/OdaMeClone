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
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "OdaMeClone",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "OdaMeClone",
                table: "Roles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "OdaMeClone",
                table: "RolePermission",
                newName: "RolePermissionId");

            migrationBuilder.AddColumn<Guid>(
                name: "ApartmentAddOnId",
                schema: "OdaMeClone",
                table: "ApartmentAddOns",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApartmentAddOnId",
                schema: "OdaMeClone",
                table: "ApartmentAddOns");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "OdaMeClone",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "OdaMeClone",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RolePermissionId",
                schema: "OdaMeClone",
                table: "RolePermission",
                newName: "Id");
        }
    }
}
