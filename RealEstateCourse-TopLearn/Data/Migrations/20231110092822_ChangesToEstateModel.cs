using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateCourse_TopLearn.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToEstateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estate_Category_CategoryId",
                table: "Estate");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Estate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Estate_Category_CategoryId",
                table: "Estate",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estate_Category_CategoryId",
                table: "Estate");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Estate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Estate_Category_CategoryId",
                table: "Estate",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
