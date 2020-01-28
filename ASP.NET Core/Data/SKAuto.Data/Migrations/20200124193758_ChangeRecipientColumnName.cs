using Microsoft.EntityFrameworkCore.Migrations;

namespace SKAuto.Data.Migrations
{
    public partial class ChangeRecipientColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Town",
                table: "Recipients",
                newName: "RecipientTown");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipientTown",
                table: "Recipients",
                newName: "Town");
        }
    }
}
