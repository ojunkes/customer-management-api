using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customers.Management.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AlterTable_Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Customer",
                newName: "Street");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Customer",
                newName: "Address");
        }
    }
}
