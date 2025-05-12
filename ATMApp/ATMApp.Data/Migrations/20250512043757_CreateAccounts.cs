using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATMApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    account_number = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    routing_number = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false),
                    account_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    inserted_at = table.Column<DateTime>(type: "timestamptz", nullable: true, defaultValueSql: "now()"),
                    inactive_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    inserted_by = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accounts_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_accounts_entity_kinds_account_type_id",
                        column: x => x.account_type_id,
                        principalTable: "entity_kinds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_accounts_name_account_number",
                table: "accounts",
                columns: new[] { "account_number", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_accounts_account_type_id",
                table: "accounts",
                column: "account_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts");
        }
    }
}
