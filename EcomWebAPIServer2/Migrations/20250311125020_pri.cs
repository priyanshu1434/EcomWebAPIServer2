﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcomWebAPIServer2.Migrations
{
    /// <inheritdoc />
    public partial class pri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Payments");
        }
    }
}
