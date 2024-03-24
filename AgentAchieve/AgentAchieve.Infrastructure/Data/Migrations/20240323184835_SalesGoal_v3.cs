using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgentAchieve.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SalesGoal_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GoalDate",
                table: "SalesGoals",
                newName: "GoalMonthYear");

            migrationBuilder.RenameColumn(
                name: "GoalAmount",
                table: "SalesGoals",
                newName: "SalesGoalAmount");

            migrationBuilder.RenameIndex(
                name: "IX_SalesGoals_OwnedById_GoalDate",
                table: "SalesGoals",
                newName: "IX_SalesGoals_OwnedById_GoalMonthYear");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalesGoalAmount",
                table: "SalesGoals",
                newName: "GoalAmount");

            migrationBuilder.RenameColumn(
                name: "GoalMonthYear",
                table: "SalesGoals",
                newName: "GoalDate");

            migrationBuilder.RenameIndex(
                name: "IX_SalesGoals_OwnedById_GoalMonthYear",
                table: "SalesGoals",
                newName: "IX_SalesGoals_OwnedById_GoalDate");
        }
    }
}
