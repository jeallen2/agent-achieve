using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgentAchieve.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SalesGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GoalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OwnedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesGoals_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesGoals_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesGoals_AspNetUsers_OwnedById",
                        column: x => x.OwnedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesGoals_CreatedById",
                table: "SalesGoals",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SalesGoals_LastModifiedById",
                table: "SalesGoals",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SalesGoals_OwnedById",
                table: "SalesGoals",
                column: "OwnedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesGoals");
        }
    }
}
