using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMallBE.Migrations
{
    /// <inheritdoc />
    public partial class AddContractToShopAndTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rentalfee = table.Column<decimal>(name: "Rental_fee", type: "decimal(18,2)", nullable: false),
                    Startdate = table.Column<DateTime>(name: "Start_date", type: "datetime2", nullable: false),
                    Enddate = table.Column<DateTime>(name: "End_date", type: "datetime2", nullable: false),
                    Termsandconditions = table.Column<string>(name: "Terms_and_conditions", type: "nvarchar(max)", nullable: false),
                    Additionalnotes = table.Column<string>(name: "Additional_notes", type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<int>(name: "Tenant_Id", type: "int", nullable: false),
                    TenantId0 = table.Column<int>(name: "TenantId", type: "int", nullable: false),
                    ShopId = table.Column<int>(name: "Shop_Id", type: "int", nullable: false),
                    ShopId0 = table.Column<int>(name: "ShopId", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Shops_ShopId",
                        column: x => x.ShopId0,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Tenants_TenantId",
                        column: x => x.TenantId0,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ShopId",
                table: "Contracts",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TenantId",
                table: "Contracts",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}
