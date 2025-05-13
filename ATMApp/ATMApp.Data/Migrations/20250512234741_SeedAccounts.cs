using ATMApp.Data.Seeds;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATMApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AccountSeed.Up();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            AccountSeed.Down();
        }
    }
}
