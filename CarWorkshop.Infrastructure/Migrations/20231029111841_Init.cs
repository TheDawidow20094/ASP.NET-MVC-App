using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWorkshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarWorkshops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ContactDetails_PhoneNo = table.Column<string>(type: "TEXT", nullable: true),
                    ContactDetails_Street = table.Column<string>(type: "TEXT", nullable: true),
                    ContactDetails_City = table.Column<string>(type: "TEXT", nullable: true),
                    ContactDetails_PostalCode = table.Column<string>(type: "TEXT", nullable: true),
                    EncodedName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWorkshops", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarWorkshops");
        }
    }
}
