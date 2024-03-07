using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Migrations.Product
{
    /// <inheritdoc />
    public partial class three : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    TotalProq = table.Column<int>(type: "INTEGER", nullable: false),
                    PricePerq = table.Column<double>(type: "REAL", nullable: false),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: false),
                    Discount = table.Column<double>(type: "REAL", nullable: false),
                    CategoryId = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryQ = table.Column<int>(type: "INTEGER", nullable: false),
                    CusPhonenumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
