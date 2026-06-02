using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RespiraAMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProtocolWithName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "treatment_protocols",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "treatment_protocols");
        }
    }
}
