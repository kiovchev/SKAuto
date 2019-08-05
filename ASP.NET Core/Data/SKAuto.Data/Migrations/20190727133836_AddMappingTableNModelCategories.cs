namespace SKAuto.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddMappingTableNModelCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Models_ModelId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ModelId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "ModelsCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    ModelId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelsCategories", x => new { x.CategoryId, x.ModelId });
                    table.ForeignKey(
                        name: "FK_ModelsCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModelsCategories_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModelsCategories_ModelId",
                table: "ModelsCategories",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModelsCategories");

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "Categories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ModelId",
                table: "Categories",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Models_ModelId",
                table: "Categories",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
