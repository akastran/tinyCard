using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tinyCard.app.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    CardNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CardPresentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EcommerceBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.CardNumber);
                });

            migrationBuilder.CreateTable(
                name: "Limit",
                columns: table => new
                {
                    LimitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TranType = table.Column<int>(type: "int", nullable: false),
                    LimitDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TranTypeLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TranTypeBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limit", x => x.LimitId);
                    table.ForeignKey(
                        name: "FK_Limit_Card_CardNumber",
                        column: x => x.CardNumber,
                        principalTable: "Card",
                        principalColumn: "CardNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Limit_CardNumber",
                table: "Limit",
                column: "CardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Limit_LimitId",
                table: "Limit",
                column: "LimitId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Limit");

            migrationBuilder.DropTable(
                name: "Card");
        }
    }
}
