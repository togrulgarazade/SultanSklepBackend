using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SultanSklepBackend.Migrations
{
    public partial class FourthMigrationForFixSomeBugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(HOUR, 1, GETUTCDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "ContactAdmin",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(HOUR, 1, GETUTCDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(HOUR, 1, GETUTCDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(HOUR, 1, GETUTCDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(HOUR, 1, GETUTCDATE())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "ContactAdmin",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(HOUR, 1, GETUTCDATE())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(HOUR, 1, GETUTCDATE())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(HOUR, 1, GETUTCDATE())");
        }
    }
}
