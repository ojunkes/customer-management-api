using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customers.Management.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CreaterTable_Address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateInitials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IbgeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiafiCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_ZipCode",
                table: "Address",
                column: "ZipCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
