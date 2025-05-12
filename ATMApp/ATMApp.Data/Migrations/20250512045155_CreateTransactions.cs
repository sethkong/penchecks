using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATMApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    posting_date = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    transaction_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(12,4)", nullable: false),
                    balance = table.Column<decimal>(type: "numeric(12,4)", nullable: false),
                    reconcile = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    inserted_at = table.Column<DateTime>(type: "timestamptz", nullable: true, defaultValueSql: "now()"),
                    inactive_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    inserted_by = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transactions_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_transactions_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_transactions_entity_kinds_transaction_type_id",
                        column: x => x.transaction_type_id,
                        principalTable: "entity_kinds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_transactions_account_id",
                table: "transactions",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "ix_transactions_transaction_type_id",
                table: "transactions",
                column: "transaction_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions");
        }
    }
}
