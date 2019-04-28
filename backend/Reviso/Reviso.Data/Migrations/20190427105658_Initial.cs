using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reviso.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(nullable: false),
                    BaseRate = table.Column<decimal>(nullable: false),
                    VatRate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hours = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeRegistration_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ContractId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RateInterval",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractId = table.Column<int>(nullable: false),
                    FromHours = table.Column<int>(nullable: false),
                    ToHours = table.Column<int>(nullable: false),
                    DiscountFactor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateInterval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RateInterval_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "End", "Name", "Start" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portal Solution", new DateTime(2019, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "End", "Name", "Start" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Version 2.0", new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "BaseRate", "ProjectId", "VatRate" },
                values: new object[,]
                {
                    { 1, 100m, 1, 0.22m },
                    { 2, 110m, 2, 0.18m }
                });

            migrationBuilder.InsertData(
                table: "TimeRegistration",
                columns: new[] { "Id", "Date", "Hours", "ProjectId" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1 },
                    { 2, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 1 },
                    { 3, new DateTime(2019, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 1 },
                    { 4, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2 },
                    { 5, new DateTime(2019, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 2 }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "ContractId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Acme A/S" },
                    { 2, 2, "Insurance Inc." }
                });

            migrationBuilder.InsertData(
                table: "RateInterval",
                columns: new[] { "Id", "ContractId", "DiscountFactor", "FromHours", "ToHours" },
                values: new object[,]
                {
                    { 1, 1, 0m, 0, 2147483647 },
                    { 2, 2, 0m, 0, 2147483647 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ProjectId",
                table: "Contract",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ContractId",
                table: "Customer",
                column: "ContractId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RateInterval_ContractId",
                table: "RateInterval",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistration_ProjectId",
                table: "TimeRegistration",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "RateInterval");

            migrationBuilder.DropTable(
                name: "TimeRegistration");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
