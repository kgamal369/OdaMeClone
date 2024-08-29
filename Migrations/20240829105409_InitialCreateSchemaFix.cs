using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OdaMeClone.Migrations
    {
    /// <inheritdoc />
    public partial class InitialCreateSchemaFix : Migration
        {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
            {
            migrationBuilder.EnsureSchema(
                name: "OdaMeClone");

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "OdaMeClone",
                columns: table => new
                    {
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ContactNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "OdaMeClone",
                columns: table => new
                    {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                schema: "OdaMeClone",
                columns: table => new
                    {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    Permission = table.Column<string>(type: "text", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermission_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "OdaMeClone",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "OdaMeClone",
                columns: table => new
                    {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorCode = table.Column<string>(type: "text", nullable: true),
                    TwoFactorCodeExpiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "text", nullable: true),
                    PasswordResetTokenExpiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "OdaMeClone",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "OdaMeClone",
                columns: table => new
                    {
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Amenities = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    TotalUnits = table.Column<int>(type: "integer", nullable: false),
                    ProjectLogo = table.Column<byte[]>(type: "bytea", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "OdaMeClone",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AddOns",
                schema: "OdaMeClone",
                columns: table => new
                    {
                    AddOnId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddOnName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AddOnType = table.Column<int>(type: "integer", nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    MaxUnits = table.Column<int>(type: "integer", nullable: false),
                    InstalledUnits = table.Column<int>(type: "integer", nullable: false),
                    ApartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    ApartmentId1 = table.Column<Guid>(type: "uuid", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddOns", x => x.AddOnId);
                });

            migrationBuilder.CreateTable(
                name: "ApartmentAddOns",
                schema: "OdaMeClone",
                columns: table => new
                    {
                    ApartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddOnId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstalledUnits = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentAddOns", x => new { x.ApartmentId, x.AddOnId });
                    table.ForeignKey(
                        name: "FK_ApartmentAddOns_AddOns_AddOnId",
                        column: x => x.AddOnId,
                        principalSchema: "OdaMeClone",
                        principalTable: "AddOns",
                        principalColumn: "AddOnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Apartments",
                schema: "OdaMeClone",
                columns: table => new
                    {
                    ApartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApartmentName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ApartmentType = table.Column<int>(type: "integer", nullable: false),
                    Space = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ApartmentPhotos = table.Column<List<byte[]>>(type: "bytea[]", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedPackageId = table.Column<Guid>(type: "uuid", nullable: true),
                    FloorNumber = table.Column<int>(type: "integer", nullable: false),
                    ViewType = table.Column<string>(type: "text", nullable: false),
                    AvailabilityDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.ApartmentId);
                    table.ForeignKey(
                        name: "FK_Apartments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "OdaMeClone",
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Apartments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "OdaMeClone",
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                schema: "OdaMeClone",
                columns: table => new
                    {
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AssignedPerson = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalSchema: "OdaMeClone",
                        principalTable: "Apartments",
                        principalColumn: "ApartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "OdaMeClone",
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                schema: "OdaMeClone",
                columns: table => new
                    {
                    PackageId = table.Column<Guid>(type: "uuid", nullable: false),
                    PackageName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PackageType = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ApartmentId = table.Column<Guid>(type: "uuid", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageId);
                    table.ForeignKey(
                        name: "FK_Packages_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalSchema: "OdaMeClone",
                        principalTable: "Apartments",
                        principalColumn: "ApartmentId");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "OdaMeClone",
                columns: table => new
                    {
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalSchema: "OdaMeClone",
                        principalTable: "Apartments",
                        principalColumn: "ApartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalSchema: "OdaMeClone",
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "OdaMeClone",
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "OdaMeClone",
                columns: table => new
                    {
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "OdaMeClone",
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "OdaMeClone",
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddOns_ApartmentId",
                schema: "OdaMeClone",
                table: "AddOns",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AddOns_ApartmentId1",
                schema: "OdaMeClone",
                table: "AddOns",
                column: "ApartmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentAddOns_AddOnId",
                schema: "OdaMeClone",
                table: "ApartmentAddOns",
                column: "AddOnId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_AssignedPackageId",
                schema: "OdaMeClone",
                table: "Apartments",
                column: "AssignedPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_CustomerId",
                schema: "OdaMeClone",
                table: "Apartments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_ProjectId",
                schema: "OdaMeClone",
                table: "Apartments",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ApartmentId",
                schema: "OdaMeClone",
                table: "Bookings",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                schema: "OdaMeClone",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ApartmentId",
                schema: "OdaMeClone",
                table: "Invoices",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BookingId",
                schema: "OdaMeClone",
                table: "Invoices",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                schema: "OdaMeClone",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_ApartmentId",
                schema: "OdaMeClone",
                table: "Packages",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CustomerId",
                schema: "OdaMeClone",
                table: "Payments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                schema: "OdaMeClone",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                schema: "OdaMeClone",
                table: "Projects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                schema: "OdaMeClone",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "OdaMeClone",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddOns_Apartments_ApartmentId",
                schema: "OdaMeClone",
                table: "AddOns",
                column: "ApartmentId",
                principalSchema: "OdaMeClone",
                principalTable: "Apartments",
                principalColumn: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddOns_Apartments_ApartmentId1",
                schema: "OdaMeClone",
                table: "AddOns",
                column: "ApartmentId1",
                principalSchema: "OdaMeClone",
                principalTable: "Apartments",
                principalColumn: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentAddOns_Apartments_ApartmentId",
                schema: "OdaMeClone",
                table: "ApartmentAddOns",
                column: "ApartmentId",
                principalSchema: "OdaMeClone",
                principalTable: "Apartments",
                principalColumn: "ApartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Packages_AssignedPackageId",
                schema: "OdaMeClone",
                table: "Apartments",
                column: "AssignedPackageId",
                principalSchema: "OdaMeClone",
                principalTable: "Packages",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.SetNull);
            }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
            {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Apartments_ApartmentId",
                schema: "OdaMeClone",
                table: "Packages");

            migrationBuilder.DropTable(
                name: "ApartmentAddOns",
                schema: "OdaMeClone");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "OdaMeClone");

            migrationBuilder.DropTable(
                name: "RolePermission",
                schema: "OdaMeClone");

            migrationBuilder.DropTable(
                name: "AddOns",
                schema: "OdaMeClone");

            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "OdaMeClone");

            migrationBuilder.DropTable(
                name: "Bookings",
                schema: "OdaMeClone");

            migrationBuilder.DropTable(
                name: "Apartments",
                schema: "OdaMeClone");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "OdaMeClone");

            migrationBuilder.DropTable(
                name: "Packages",
                schema: "OdaMeClone");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "OdaMeClone");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "OdaMeClone");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "OdaMeClone");
            }
        }
    }
