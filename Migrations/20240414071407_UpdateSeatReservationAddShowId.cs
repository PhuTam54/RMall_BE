using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMallBE.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeatReservationAddShowId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShowId",
                table: "SeatReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Show_Id",
                table: "SeatReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated_At",
                table: "Reviews",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deleted_At",
                table: "Reviews",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_SeatReservations_ShowId",
                table: "SeatReservations",
                column: "ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatReservations_Shows_ShowId",
                table: "SeatReservations",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatReservations_Shows_ShowId",
                table: "SeatReservations");

            migrationBuilder.DropIndex(
                name: "IX_SeatReservations_ShowId",
                table: "SeatReservations");

            migrationBuilder.DropColumn(
                name: "ShowId",
                table: "SeatReservations");

            migrationBuilder.DropColumn(
                name: "Show_Id",
                table: "SeatReservations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated_At",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deleted_At",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
