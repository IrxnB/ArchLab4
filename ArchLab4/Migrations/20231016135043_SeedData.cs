using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ArchLab4.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Employees",
                newName: "IsActive");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "IsActive", "Name", "Title" },
                values: new object[,]
                {
                    { 1L, true, "John", "Manager" },
                    { 2L, true, "Alice", "Developer" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "EmployeeId", "IsCompleted", "Name" },
                values: new object[,]
                {
                    { 1L, "Description 1", 1L, false, "Task 1" },
                    { 2L, "Description 2", 2L, true, "Task 2" },
                    { 3L, "Description 3", 1L, false, "Task 3" },
                    { 4L, "Description 4", 2L, true, "Task 4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Employees",
                newName: "isActive");
        }
    }
}
