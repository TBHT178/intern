using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentID);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PointOfLoading = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PointOfUnloading = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsFlightCompleted = table.Column<bool>(type: "bit", nullable: false),
                    TotalDocuments = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightID);
                });

            migrationBuilder.CreateTable(
                name: "PermissionGroups",
                columns: table => new
                {
                    PermissionGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroups", x => x.PermissionGroupID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "FlightDocuments",
                columns: table => new
                {
                    FlightDocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightID = table.Column<int>(type: "int", nullable: false),
                    DocumentID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightDocuments", x => x.FlightDocumentID);
                    table.ForeignKey(
                        name: "FK_FlightDocuments_Documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "Documents",
                        principalColumn: "DocumentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightDocuments_Flights_FlightID",
                        column: x => x.FlightID,
                        principalTable: "Flights",
                        principalColumn: "FlightID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentID = table.Column<int>(type: "int", nullable: false),
                    PermissionGroupID = table.Column<int>(type: "int", nullable: false),
                    CanView = table.Column<bool>(type: "bit", nullable: false),
                    CanEdit = table.Column<bool>(type: "bit", nullable: false),
                    CanDownload = table.Column<bool>(type: "bit", nullable: false),
                    NoPermission = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionID);
                    table.ForeignKey(
                        name: "FK_Permissions_Documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "Documents",
                        principalColumn: "DocumentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_PermissionGroups_PermissionGroupID",
                        column: x => x.PermissionGroupID,
                        principalTable: "PermissionGroups",
                        principalColumn: "PermissionGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PermissionGroups",
                columns: new[] { "PermissionGroupID", "GroupName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Back-Office" },
                    { 3, "Pilot" },
                    { 4, "Crew" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedAt", "Email", "FullName", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 23, 23, 11, 13, 590, DateTimeKind.Local).AddTicks(2846), "admin@vietjetair.com", "Admin User", "$2a$11$CN4fKGg0smrKagOUYCnTaunpqbBayRG/yq1K/spt5mtheKJolhwzq", "1d3afffd-c52e-4a85-8ecf-2a10356d6855", new DateTime(2024, 10, 30, 23, 11, 13, 702, DateTimeKind.Local).AddTicks(8108), "Admin", "Active" },
                    { 2, new DateTime(2024, 10, 23, 23, 11, 13, 702, DateTimeKind.Local).AddTicks(8134), "backoffice@vietjetair.com", "BackOffice User", "$2a$11$0PO5V3JuZKXntFJAT4Nj4uREHUbp3KlnVReiohh9wt3FcfI7nM4wq", "4f688d0c-105c-47a2-b438-1c302dc8805c", new DateTime(2024, 10, 30, 23, 11, 13, 815, DateTimeKind.Local).AddTicks(9683), "Back-Office", "Active" },
                    { 3, new DateTime(2024, 10, 23, 23, 11, 13, 815, DateTimeKind.Local).AddTicks(9708), "pilot@vietjetair.com", "Pilot User", "$2a$11$KZ5E63uqLBXcqdaaCWMhgOHooCD.AY2w5rZMdA5ipVXWhNwM7fPha", "e4b5f807-2c08-4ef6-9778-54455732b715", new DateTime(2024, 10, 30, 23, 11, 13, 931, DateTimeKind.Local).AddTicks(959), "Pilot", "Active" },
                    { 4, new DateTime(2024, 10, 23, 23, 11, 13, 931, DateTimeKind.Local).AddTicks(996), "crew@vietjetair.com", "Crew User", "$2a$11$8CMphSrSFZ5Q3qCeoLtmv.hfx4lGwp.EPfMs2EyWU6m6bfETup6vO", "0136e748-3d53-4435-9e94-0715fc4b5bdd", new DateTime(2024, 10, 30, 23, 11, 14, 43, DateTimeKind.Local).AddTicks(9969), "Crew", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightDocuments_DocumentID",
                table: "FlightDocuments",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_FlightDocuments_FlightID",
                table: "FlightDocuments",
                column: "FlightID");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_DocumentID",
                table: "Permissions",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionGroupID",
                table: "Permissions",
                column: "PermissionGroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightDocuments");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "PermissionGroups");
        }
    }
}
