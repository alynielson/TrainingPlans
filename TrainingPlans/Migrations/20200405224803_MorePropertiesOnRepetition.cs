using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlans.Migrations
{
    public partial class MorePropertiesOnRepetition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityType",
                table: "PlannedRepetition",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PlannedRepetition",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActivityType",
                table: "CompletedRepetition",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CompletedRepetition",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserDefaults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistanceUom = table.Column<int>(nullable: false),
                    TimeUom = table.Column<int>(nullable: false),
                    IsPaceDistancePerTime = table.Column<bool>(nullable: false),
                    Pace = table.Column<double>(nullable: false),
                    ActivityType = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDefaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDefaults_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlannedRepetition_UserId",
                table: "PlannedRepetition",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedRepetition_UserId",
                table: "CompletedRepetition",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDefaults_UserId",
                table: "UserDefaults",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedRepetition_User_UserId",
                table: "CompletedRepetition",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedRepetition_User_UserId",
                table: "PlannedRepetition",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedRepetition_User_UserId",
                table: "CompletedRepetition");

            migrationBuilder.DropForeignKey(
                name: "FK_PlannedRepetition_User_UserId",
                table: "PlannedRepetition");

            migrationBuilder.DropTable(
                name: "UserDefaults");

            migrationBuilder.DropIndex(
                name: "IX_PlannedRepetition_UserId",
                table: "PlannedRepetition");

            migrationBuilder.DropIndex(
                name: "IX_CompletedRepetition_UserId",
                table: "CompletedRepetition");

            migrationBuilder.DropColumn(
                name: "ActivityType",
                table: "PlannedRepetition");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PlannedRepetition");

            migrationBuilder.DropColumn(
                name: "ActivityType",
                table: "CompletedRepetition");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CompletedRepetition");
        }
    }
}
