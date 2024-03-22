using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgentAchieve.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SalesGoal_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SalesGoals_OwnedById",
                table: "SalesGoals");

            migrationBuilder.CreateIndex(
                name: "IX_SalesGoals_OwnedById_GoalDate",
                table: "SalesGoals",
                columns: new[] { "OwnedById", "GoalDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SalesGoals_OwnedById_GoalDate",
                table: "SalesGoals");

            migrationBuilder.CreateIndex(
                name: "IX_SalesGoals_OwnedById",
                table: "SalesGoals",
                column: "OwnedById");
        }
    }
}
