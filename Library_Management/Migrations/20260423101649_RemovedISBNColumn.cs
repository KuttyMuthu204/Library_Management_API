using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management.Migrations
{
    /// <inheritdoc />
    public partial class RemovedISBNColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Bookes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Bookes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
