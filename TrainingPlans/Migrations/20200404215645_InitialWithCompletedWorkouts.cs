using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlans.Migrations
{
    public partial class InitialWithCompletedWorkouts : Migration
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
                    Order = table.Column<int>(nullable: false),
                    ActivityType = table.Column<int>(nullable: false),
                    WorkoutType = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    TimeOfDay = table.Column<int>(nullable: false),
                    ScheduledDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedWorkout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlannedWorkout_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CompletedWorkout",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ActivityType = table.Column<int>(nullable: false),
                    WorkoutType = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    CompletedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    PlannedWorkoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedWorkout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedWorkout_PlannedWorkout_PlannedWorkoutId",
                        column: x => x.PlannedWorkoutId,
                        principalTable: "PlannedWorkout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CompletedWorkout_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    RestDistanceQuantity = table.Column<double>(nullable: true),
                    RestDistanceUom = table.Column<int>(nullable: true),
                    RestTimeQuantity = table.Column<double>(nullable: true),
                    RestTimeUom = table.Column<int>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    PlannedWorkoutId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "CompletedRepetition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistanceQuantity = table.Column<double>(nullable: true),
                    DistanceUom = table.Column<int>(nullable: true),
                    TimeQuantity = table.Column<double>(nullable: true),
                    TimeUom = table.Column<int>(nullable: true),
                    RestDistanceQuantity = table.Column<double>(nullable: true),
                    RestDistanceUom = table.Column<int>(nullable: true),
                    RestTimeQuantity = table.Column<double>(nullable: true),
                    RestTimeUom = table.Column<int>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    PlannedRepetitionId = table.Column<int>(nullable: true),
                    CompletedWorkoutId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedRepetition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedRepetition_CompletedWorkout_CompletedWorkoutId",
                        column: x => x.CompletedWorkoutId,
                        principalTable: "CompletedWorkout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompletedRepetition_PlannedRepetition_PlannedRepetitionId",
                        column: x => x.PlannedRepetitionId,
                        principalTable: "PlannedRepetition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedRepetition_CompletedWorkoutId",
                table: "CompletedRepetition",
                column: "CompletedWorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedRepetition_PlannedRepetitionId",
                table: "CompletedRepetition",
                column: "PlannedRepetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedWorkout_PlannedWorkoutId",
                table: "CompletedWorkout",
                column: "PlannedWorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedWorkout_UserId",
                table: "CompletedWorkout",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedRepetition_PlannedWorkoutId",
                table: "PlannedRepetition",
                column: "PlannedWorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedWorkout_UserId",
                table: "PlannedWorkout",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedRepetition");

            migrationBuilder.DropTable(
                name: "CompletedWorkout");

            migrationBuilder.DropTable(
                name: "PlannedRepetition");

            migrationBuilder.DropTable(
                name: "PlannedWorkout");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
