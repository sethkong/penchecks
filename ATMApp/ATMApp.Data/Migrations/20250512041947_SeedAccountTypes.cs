using ATMApp.Data.Seeds;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATMApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedAccountTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AccountTypeSeed.Up();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            AccountTypeSeed.Down();
        }
    }
}
