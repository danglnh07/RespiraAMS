using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RespiraAMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProtocolIssueDayType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "IssueDate",
                table: "treatment_protocols",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "IssueDate",
                table: "treatment_protocols",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
