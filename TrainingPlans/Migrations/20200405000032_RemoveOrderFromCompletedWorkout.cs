using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingPlans.Migrations
{
    public partial class RemoveOrderFromCompletedWorkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "CompletedWorkout");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CompletedWorkout",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
