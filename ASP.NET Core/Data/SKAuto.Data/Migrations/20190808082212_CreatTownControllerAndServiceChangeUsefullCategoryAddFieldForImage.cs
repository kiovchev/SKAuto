namespace SKAuto.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CreatTownControllerAndServiceChangeUsefullCategoryAddFieldForImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UseFullCategories",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageAddress",
                table: "UseFullCategories",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Towns",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageAddress",
                table: "UseFullCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UseFullCategories",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Towns",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
