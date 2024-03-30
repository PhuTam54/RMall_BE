using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMallBE.Migrations
{
    /// <inheritdoc />
    public partial class changeForeignkeySeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatReservations_SeatReservationId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_SeatReservationId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "SeatReservationId",
                table: "Seats");

            migrationBuilder.AddColumn<int>(
                name: "Seat_Id",
                table: "SeatReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeatsId",
                table: "SeatReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SeatReservations_SeatsId",
                table: "SeatReservations",
                column: "SeatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatReservations_Seats_SeatsId",
                table: "SeatReservations",
                column: "SeatsId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatReservations_Seats_SeatsId",
                table: "SeatReservations");

            migrationBuilder.DropIndex(
                name: "IX_SeatReservations_SeatsId",
                table: "SeatReservations");

            migrationBuilder.DropColumn(
                name: "Seat_Id",
                table: "SeatReservations");

            migrationBuilder.DropColumn(
                name: "SeatsId",
                table: "SeatReservations");

            migrationBuilder.AddColumn<int>(
                name: "SeatReservationId",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SeatReservationId",
                table: "Seats",
                column: "SeatReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatReservations_SeatReservationId",
                table: "Seats",
                column: "SeatReservationId",
                principalTable: "SeatReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
