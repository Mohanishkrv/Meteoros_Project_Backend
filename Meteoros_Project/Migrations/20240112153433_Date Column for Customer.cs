using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeteorosProject.Migrations
{
    /// <inheritdoc />
    public partial class DateColumnforCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Customers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Customers");
        }
    }
}
