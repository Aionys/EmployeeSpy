using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeSpy.DataAccessEF.Migrations
{
    public partial class GateKeeperNoDoor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateKeepers_Doors_DoorId",
                table: "GateKeepers");

            migrationBuilder.DropIndex(
                name: "IX_GateKeepers_DoorId",
                table: "GateKeepers");

            migrationBuilder.DropColumn(
                name: "DoorId",
                table: "GateKeepers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoorId",
                table: "GateKeepers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GateKeepers_DoorId",
                table: "GateKeepers",
                column: "DoorId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateKeepers_Doors_DoorId",
                table: "GateKeepers",
                column: "DoorId",
                principalTable: "Doors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
