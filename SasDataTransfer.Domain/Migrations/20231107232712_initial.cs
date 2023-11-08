using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SasDataTransfer.Domain.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Protocol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProtocolName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protocol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Analysis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnalysisName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProtocolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analysis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analysis_Protocol_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "Protocol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InputLoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputLoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SjmJobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTransferSuccessful = table.Column<bool>(type: "bit", nullable: true),
                    AnalysisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfer_Analysis_AnalysisId",
                        column: x => x.AnalysisId,
                        principalTable: "Analysis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SasDataSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataSetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SasDataSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SasDataSet_Transfer_TransferId",
                        column: x => x.TransferId,
                        principalTable: "Transfer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatasetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variable_SasDataSet_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "SasDataSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analysis_AnalysisName",
                table: "Analysis",
                column: "AnalysisName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Analysis_ProtocolId",
                table: "Analysis",
                column: "ProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_Protocol_ProtocolName",
                table: "Protocol",
                column: "ProtocolName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SasDataSet_TransferId",
                table: "SasDataSet",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_AnalysisId",
                table: "Transfer",
                column: "AnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_Variable_DatasetId",
                table: "Variable",
                column: "DatasetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Variable");

            migrationBuilder.DropTable(
                name: "SasDataSet");

            migrationBuilder.DropTable(
                name: "Transfer");

            migrationBuilder.DropTable(
                name: "Analysis");

            migrationBuilder.DropTable(
                name: "Protocol");
        }
    }
}
