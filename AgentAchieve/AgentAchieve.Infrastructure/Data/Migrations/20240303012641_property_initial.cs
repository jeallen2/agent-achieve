using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgentAchieve.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class property_initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YearBuilt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Bedrooms = table.Column<int>(type: "int", nullable: true),
                    FullBathrooms = table.Column<int>(type: "int", nullable: true),
                    HalfBathrooms = table.Column<int>(type: "int", nullable: true),
                    SquareFootage = table.Column<int>(type: "int", nullable: true),
                    NumberOfLevels = table.Column<int>(type: "int", nullable: true),
                    LotSize = table.Column<int>(type: "int", nullable: true),
                    PropertyType = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Property");
        }
    }
}
