using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsService.Database.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddedTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Availability", "Brand", "Model", "Power", "Price", "RegistrationNumber", "Type" },
                values: new object[] { new Guid("109b42f3-198d-4c89-9276-a7520a7120ab"), true, "Mercedes Benz", "GLA 250", 249, 3500, "ЛО777Х799", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("109b42f3-198d-4c89-9276-a7520a7120ab"));
        }
    }
}
