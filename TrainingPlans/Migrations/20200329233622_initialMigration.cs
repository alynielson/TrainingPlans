using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlans.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlannedWorkout",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    TimeOfDay = table.Column<int>(nullable: false),
                    ScheduledDate = table.Column<DateTime>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ActivityType = table.Column<int>(nullable: false),
                    WorkoutType = table.Column<int>(nullable: false),
                    AthleteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedWorkout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlannedWorkout_User_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlannedRepetition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistanceQuantity = table.Column<double>(nullable: true),
                    DistanceUom = table.Column<int>(nullable: true),
                    TimeQuantity = table.Column<double>(nullable: true),
                    TimeUom = table.Column<int>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    PlannedWorkoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedRepetition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlannedRepetition_PlannedWorkout_PlannedWorkoutId",
                        column: x => x.PlannedWorkoutId,
                        principalTable: "PlannedWorkout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlannedRepetition_PlannedWorkoutId",
                table: "PlannedRepetition",
                column: "PlannedWorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedWorkout_AthleteId",
                table: "PlannedWorkout",
                column: "AthleteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlannedRepetition");

            migrationBuilder.DropTable(
                name: "PlannedWorkout");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
