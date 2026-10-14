using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddChangedByinProviderDocumentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChangedBy",
                table: "ProviderDocuments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProviderDocuments_ChangedBy",
                table: "ProviderDocuments",
                column: "ChangedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderDocuments_Users_ChangedBy",
                table: "ProviderDocuments",
                column: "ChangedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderDocuments_Users_ChangedBy",
                table: "ProviderDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ProviderDocuments_ChangedBy",
                table: "ProviderDocuments");

            migrationBuilder.DropColumn(
                name: "ChangedBy",
                table: "ProviderDocuments");
        }
    }
}
