using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shopping.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Express Galaxy Tumbler", 19.95m },
                    { 2, "Aero Life Air Purifier", 89.99m },
                    { 3, "Ocean Wave Projector", 29.95m },
                    { 4, "Illuminated Globe Decor", 42.50m },
                    { 5, "Moonlight Cushion", 24.99m },
                    { 6, "Sunrise Alarm Clock", 35.00m },
                    { 7, "Frosty Mini Fridge", 120.00m },
                    { 8, "Breeze Tower Fan", 75.89m },
                    { 9, "Comet Electric Scooter", 299.99m },
                    { 10, "Starlight Projector", 27.50m }
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
