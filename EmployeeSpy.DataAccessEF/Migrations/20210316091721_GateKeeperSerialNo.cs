using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeSpy.DataAccessEF.Migrations
{
    public partial class GateKeeperSerialNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerialNo",
                table: "GateKeepers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNo",
                table: "GateKeepers");
        }
    }
}
