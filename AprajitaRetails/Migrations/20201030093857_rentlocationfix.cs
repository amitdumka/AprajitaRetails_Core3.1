using Microsoft.EntityFrameworkCore.Migrations;

namespace AprajitaRetails.Migrations
{
    public partial class rentlocationfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_RentedLocations_LocationRentedLocationId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_LocationRentedLocationId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "LocationRentedLocationId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "RentLocationId",
                table: "Rents");

            migrationBuilder.AddColumn<int>(
                name: "RentedLocationId",
                table: "Rents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rents_RentedLocationId",
                table: "Rents",
                column: "RentedLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_RentedLocations_RentedLocationId",
                table: "Rents",
                column: "RentedLocationId",
                principalTable: "RentedLocations",
                principalColumn: "RentedLocationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_RentedLocations_RentedLocationId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_RentedLocationId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "RentedLocationId",
                table: "Rents");

            migrationBuilder.AddColumn<int>(
                name: "LocationRentedLocationId",
                table: "Rents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RentLocationId",
                table: "Rents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rents_LocationRentedLocationId",
                table: "Rents",
                column: "LocationRentedLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_RentedLocations_LocationRentedLocationId",
                table: "Rents",
                column: "LocationRentedLocationId",
                principalTable: "RentedLocations",
                principalColumn: "RentedLocationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
