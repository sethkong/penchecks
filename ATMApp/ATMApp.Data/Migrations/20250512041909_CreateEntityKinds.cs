using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATMApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntityKinds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entity_kinds",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    inserted_at = table.Column<DateTime>(type: "timestamptz", nullable: true, defaultValueSql: "now()"),
                    inactive_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    inserted_by = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entity_kinds_id", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_entity_kinds_name_code",
                table: "entity_kinds",
                columns: new[] { "name", "code" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entity_kinds");
        }
    }
}
