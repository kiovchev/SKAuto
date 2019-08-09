using Microsoft.EntityFrameworkCore.Migrations;

namespace SKAuto.Data.Migrations
{
    public partial class AddMappingTableOrdersParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Orders_OrderId",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_OrderId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Parts");

            migrationBuilder.CreateTable(
                name: "OrdersParts",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    PartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersParts", x => new { x.OrderId, x.PartId });
                    table.ForeignKey(
                        name: "FK_OrdersParts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdersParts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersParts_PartId",
                table: "OrdersParts",
                column: "PartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersParts");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Parts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_OrderId",
                table: "Parts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Orders_OrderId",
                table: "Parts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
