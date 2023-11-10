using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateCourse_TopLearn.Data.Migrations
{
    /// <inheritdoc />
    public partial class CategoryModelAndItsRelationWithEstateModelEstablished : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Estate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estate_CategoryId",
                table: "Estate",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estate_Category_CategoryId",
                table: "Estate",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estate_Category_CategoryId",
                table: "Estate");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Estate_CategoryId",
                table: "Estate");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Estate");
        }
    }
}
