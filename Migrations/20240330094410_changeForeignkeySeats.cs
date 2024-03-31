using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMallBE.Migrations
{
    /// <inheritdoc />
    public partial class changeForeignkeySeats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatReservations_Seats_SeatsId",
                table: "SeatReservations");

            migrationBuilder.RenameColumn(
                name: "SeatsId",
                table: "SeatReservations",
                newName: "SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_SeatReservations_SeatsId",
                table: "SeatReservations",
                newName: "IX_SeatReservations_SeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatReservations_Seats_SeatId",
                table: "SeatReservations",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatReservations_Seats_SeatId",
                table: "SeatReservations");

            migrationBuilder.RenameColumn(
                name: "SeatId",
                table: "SeatReservations",
                newName: "SeatsId");

            migrationBuilder.RenameIndex(
                name: "IX_SeatReservations_SeatId",
                table: "SeatReservations",
                newName: "IX_SeatReservations_SeatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatReservations_Seats_SeatsId",
                table: "SeatReservations",
                column: "SeatsId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
