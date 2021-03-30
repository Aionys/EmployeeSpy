using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeSpy.DataAccessEF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccessLevel = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkPlaceId = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Rooms_WorkPlaceId",
                        column: x => x.WorkPlaceId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeepOpenSeconds = table.Column<int>(type: "int", nullable: false, defaultValue: 10),
                    GuardId = table.Column<int>(type: "int", nullable: true),
                    EntranceControlId = table.Column<int>(type: "int", nullable: true),
                    ExitControlId = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doors_Person_GuardId",
                        column: x => x.GuardId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GateKeepers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoorId = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateKeepers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateKeepers_Doors_DoorId",
                        column: x => x.DoorId,
                        principalTable: "Doors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovementLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    PassedDoorId = table.Column<int>(type: "int", nullable: true),
                    MoveDirection = table.Column<int>(type: "int", nullable: false),
                    MoveTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovementLogs_Doors_PassedDoorId",
                        column: x => x.PassedDoorId,
                        principalTable: "Doors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovementLogs_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GateKeepers_DoorId",
                table: "GateKeepers",
                column: "DoorId");

            migrationBuilder.CreateIndex(
                name: "IX_Doors_EntranceControlId",
                table: "Doors",
                column: "EntranceControlId");

            migrationBuilder.CreateIndex(
                name: "IX_Doors_ExitControlId",
                table: "Doors",
                column: "ExitControlId");

            migrationBuilder.CreateIndex(
                name: "IX_Doors_GuardId",
                table: "Doors",
                column: "GuardId");

            migrationBuilder.CreateIndex(
                name: "IX_MovementLogs_PassedDoorId",
                table: "MovementLogs",
                column: "PassedDoorId");

            migrationBuilder.CreateIndex(
                name: "IX_MovementLogs_PersonId",
                table: "MovementLogs",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_WorkPlaceId",
                table: "Person",
                column: "WorkPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doors_GateKeepers_EntranceControlId",
                table: "Doors",
                column: "EntranceControlId",
                principalTable: "GateKeepers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doors_GateKeepers_ExitControlId",
                table: "Doors",
                column: "ExitControlId",
                principalTable: "GateKeepers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateKeepers_Doors_DoorId",
                table: "GateKeepers");

            migrationBuilder.DropTable(
                name: "MovementLogs");

            migrationBuilder.DropTable(
                name: "Doors");

            migrationBuilder.DropTable(
                name: "GateKeepers");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
