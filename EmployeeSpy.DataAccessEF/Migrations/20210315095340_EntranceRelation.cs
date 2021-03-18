using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeSpy.DataAccessEF.Migrations
{
    public partial class EntranceRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Doors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntranceId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Doors_RoomId",
                table: "Doors",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_EntranceId",
                table: "Rooms",
                column: "EntranceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Doors_EntranceId",
                table: "Rooms",
                column: "EntranceId",
                principalTable: "Doors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doors_Rooms_RoomId",
                table: "Doors",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Doors_EntranceId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Doors_Rooms_RoomId",
                table: "Doors");

            migrationBuilder.DropIndex(
                name: "IX_Doors_RoomId",
                table: "Doors");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_EntranceId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Doors");

            migrationBuilder.DropColumn(
                name: "EntranceId",
                table: "Rooms");
        }
    }
}
