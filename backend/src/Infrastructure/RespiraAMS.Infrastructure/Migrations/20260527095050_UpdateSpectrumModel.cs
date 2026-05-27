using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RespiraAMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSpectrumModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "antibiotic_spectra",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "antibiotic_spectra");
        }
    }
}
