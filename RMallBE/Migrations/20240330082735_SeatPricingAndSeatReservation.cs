using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMallBE.Migrations
{
    /// <inheritdoc />
    public partial class SeatPricingAndSeatReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatShows");

            migrationBuilder.RenameColumn(
                name: "SeatType_Id",
                table: "Seats",
                newName: "Seat_Id");

            migrationBuilder.AddColumn<int>(
                name: "SeatReservationId",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SeatPricings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatTypeId = table.Column<int>(name: "Seat_Type_Id", type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShowId = table.Column<int>(name: "Show_Id", type: "int", nullable: false),
                    SeatTypeId0 = table.Column<int>(name: "SeatTypeId", type: "int", nullable: false),
                    ShowId0 = table.Column<int>(name: "ShowId", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatPricings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatPricings_SeatTypes_SeatTypeId",
                        column: x => x.SeatTypeId0,
                        principalTable: "SeatTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatPricings_Shows_ShowId",
                        column: x => x.ShowId0,
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatReservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationExpiresAt = table.Column<DateTime>(name: "Reservation_Expires_At", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatReservations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SeatReservationId",
                table: "Seats",
                column: "SeatReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatPricings_SeatTypeId",
                table: "SeatPricings",
                column: "SeatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatPricings_ShowId",
                table: "SeatPricings",
                column: "ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatReservations_SeatReservationId",
                table: "Seats",
                column: "SeatReservationId",
                principalTable: "SeatReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatReservations_SeatReservationId",
                table: "Seats");

            migrationBuilder.DropTable(
                name: "SeatPricings");

            migrationBuilder.DropTable(
                name: "SeatReservations");

            migrationBuilder.DropIndex(
                name: "IX_Seats_SeatReservationId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "SeatReservationId",
                table: "Seats");

            migrationBuilder.RenameColumn(
                name: "Seat_Id",
                table: "Seats",
                newName: "SeatType_Id");

            migrationBuilder.CreateTable(
                name: "SeatShows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatTypeId = table.Column<int>(type: "int", nullable: false),
                    ShowId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SeatTypeId0 = table.Column<int>(name: "SeatType_Id", type: "int", nullable: false),
                    ShowId0 = table.Column<int>(name: "Show_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatShows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatShows_SeatTypes_SeatTypeId",
                        column: x => x.SeatTypeId,
                        principalTable: "SeatTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatShows_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeatShows_SeatTypeId",
                table: "SeatShows",
                column: "SeatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatShows_ShowId",
                table: "SeatShows",
                column: "ShowId");
        }
    }
}
