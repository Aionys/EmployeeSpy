using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeSpy.DataAccessEF.Migrations
{
    public partial class WorkPlaceRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Rooms_WorkPlaceId",
                table: "Person");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Rooms_WorkPlaceId",
                table: "Person",
                column: "WorkPlaceId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Rooms_WorkPlaceId",
                table: "Person");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Rooms_WorkPlaceId",
                table: "Person",
                column: "WorkPlaceId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
