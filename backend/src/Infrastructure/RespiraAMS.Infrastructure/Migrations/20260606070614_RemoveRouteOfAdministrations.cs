using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RespiraAMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRouteOfAdministrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteOfAdministrations",
                table: "antibiotics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int[]>(
                name: "RouteOfAdministrations",
                table: "antibiotics",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }
    }
}
