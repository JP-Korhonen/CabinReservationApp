using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class CabinReservationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostalCode = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostalCode);
                });

            migrationBuilder.CreateTable(
                name: "Resort",
                columns: table => new
                {
                    ResortId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResortName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resort", x => x.ResortId);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCode = table.Column<string>(nullable: false),
                    SocialSecurityNumber = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_Person_Post_PostalCode",
                        column: x => x.PostalCode,
                        principalTable: "Post",
                        principalColumn: "PostalCode");
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    ActivityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResortId = table.Column<int>(nullable: false),
                    PostalCode = table.Column<string>(nullable: true),
                    ActivityProvider = table.Column<string>(nullable: true),
                    ActivityName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ActivityPrice = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activity_Post_PostalCode",
                        column: x => x.PostalCode,
                        principalTable: "Post",
                        principalColumn: "PostalCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activity_Resort_ResortId",
                        column: x => x.ResortId,
                        principalTable: "Resort",
                        principalColumn: "ResortId");
                });

            migrationBuilder.CreateTable(
                name: "Cabin",
                columns: table => new
                {
                    CabinId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResortId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    CabinName = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    CabinPricePerDay = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Area = table.Column<decimal>(nullable: false),
                    Rooms = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabin", x => x.CabinId);
                    table.ForeignKey(
                        name: "FK_Cabin_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId");
                    table.ForeignKey(
                        name: "FK_Cabin_Post_PostalCode",
                        column: x => x.PostalCode,
                        principalTable: "Post",
                        principalColumn: "PostalCode");
                    table.ForeignKey(
                        name: "FK_Cabin_Resort_ResortId",
                        column: x => x.ResortId,
                        principalTable: "Resort",
                        principalColumn: "ResortId");
                });

            migrationBuilder.CreateTable(
                name: "CabinImage",
                columns: table => new
                {
                    CabinImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CabinId = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinImage", x => x.CabinImageId);
                    table.ForeignKey(
                        name: "FK_CabinImage_Cabin_CabinId",
                        column: x => x.CabinId,
                        principalTable: "Cabin",
                        principalColumn: "CabinId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CabinReservation",
                columns: table => new
                {
                    CabinReservationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CabinId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    ReservationBookingTime = table.Column<DateTime>(nullable: false),
                    ReservationStartDate = table.Column<DateTime>(nullable: false),
                    ReservationEndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinReservation", x => x.CabinReservationId);
                    table.ForeignKey(
                        name: "FK_CabinReservation_Cabin_CabinId",
                        column: x => x.CabinId,
                        principalTable: "Cabin",
                        principalColumn: "CabinId");
                    table.ForeignKey(
                        name: "FK_CabinReservation_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "ActivityReservation",
                columns: table => new
                {
                    ActivityReservationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CabinReservationId = table.Column<int>(nullable: false),
                    ActivityId = table.Column<int>(nullable: false),
                    ActivityReservationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityReservation", x => x.ActivityReservationId);
                    table.ForeignKey(
                        name: "FK_ActivityReservation_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "ActivityId");
                    table.ForeignKey(
                        name: "FK_ActivityReservation_CabinReservation_CabinReservationId",
                        column: x => x.CabinReservationId,
                        principalTable: "CabinReservation",
                        principalColumn: "CabinReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CabinReservationId = table.Column<int>(nullable: false),
                    DateOfExpiry = table.Column<DateTime>(nullable: false),
                    PaidYesNo = table.Column<bool>(nullable: false),
                    Vat = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    InvoiceTotalAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_CabinReservation_CabinReservationId",
                        column: x => x.CabinReservationId,
                        principalTable: "CabinReservation",
                        principalColumn: "CabinReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostalCode", "City" },
                values: new object[] { "00100", "Helsinki" });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "PersonId", "Address", "Email", "FirstName", "LastName", "PhoneNumber", "PostalCode", "SocialSecurityNumber" },
                values: new object[] { 1, "admin@localhost.org", "admin@localhost.org", "admin@localhost.org", "admin@localhost.org", "admin@localhost.org", "00100", "admin@localhost.org" });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_PostalCode",
                table: "Activity",
                column: "PostalCode");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ResortId",
                table: "Activity",
                column: "ResortId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityReservation_ActivityId",
                table: "ActivityReservation",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityReservation_CabinReservationId",
                table: "ActivityReservation",
                column: "CabinReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Cabin_PersonId",
                table: "Cabin",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Cabin_PostalCode",
                table: "Cabin",
                column: "PostalCode");

            migrationBuilder.CreateIndex(
                name: "IX_Cabin_ResortId",
                table: "Cabin",
                column: "ResortId");

            migrationBuilder.CreateIndex(
                name: "IX_CabinImage_CabinId",
                table: "CabinImage",
                column: "CabinId");

            migrationBuilder.CreateIndex(
                name: "IX_CabinImage_ImageUrl",
                table: "CabinImage",
                column: "ImageUrl",
                unique: true,
                filter: "[ImageUrl] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CabinReservation_CabinId",
                table: "CabinReservation",
                column: "CabinId");

            migrationBuilder.CreateIndex(
                name: "IX_CabinReservation_PersonId",
                table: "CabinReservation",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CabinReservationId",
                table: "Invoice",
                column: "CabinReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PostalCode",
                table: "Person",
                column: "PostalCode");

            migrationBuilder.CreateIndex(
                name: "IX_Resort_ResortName",
                table: "Resort",
                column: "ResortName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityReservation");

            migrationBuilder.DropTable(
                name: "CabinImage");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "CabinReservation");

            migrationBuilder.DropTable(
                name: "Cabin");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Resort");

            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
