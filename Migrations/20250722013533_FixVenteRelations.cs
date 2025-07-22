using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaissePoly.Migrations
{
    /// <inheritdoc />
    public partial class FixVenteRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vente_Article_ArticleidA",
                table: "Vente");

            migrationBuilder.DropForeignKey(
                name: "FK_Vente_Tickets_TicketIdT",
                table: "Vente");

            migrationBuilder.DropIndex(
                name: "IX_Vente_TicketIdT",
                table: "Vente");

            migrationBuilder.DropColumn(
                name: "TicketIdT",
                table: "Vente");

            migrationBuilder.RenameColumn(
                name: "ArticleidA",
                table: "Vente",
                newName: "IdT");

            migrationBuilder.RenameIndex(
                name: "IX_Vente_ArticleidA",
                table: "Vente",
                newName: "IX_Vente_IdT");

            migrationBuilder.CreateIndex(
                name: "IX_Vente_IdA",
                table: "Vente",
                column: "IdA");

            migrationBuilder.AddForeignKey(
                name: "FK_Vente_Article_IdA",
                table: "Vente",
                column: "IdA",
                principalTable: "Article",
                principalColumn: "idA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vente_Tickets_IdT",
                table: "Vente",
                column: "IdT",
                principalTable: "Tickets",
                principalColumn: "IdT",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vente_Article_IdA",
                table: "Vente");

            migrationBuilder.DropForeignKey(
                name: "FK_Vente_Tickets_IdT",
                table: "Vente");

            migrationBuilder.DropIndex(
                name: "IX_Vente_IdA",
                table: "Vente");

            migrationBuilder.RenameColumn(
                name: "IdT",
                table: "Vente",
                newName: "ArticleidA");

            migrationBuilder.RenameIndex(
                name: "IX_Vente_IdT",
                table: "Vente",
                newName: "IX_Vente_ArticleidA");

            migrationBuilder.AddColumn<int>(
                name: "TicketIdT",
                table: "Vente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vente_TicketIdT",
                table: "Vente",
                column: "TicketIdT");

            migrationBuilder.AddForeignKey(
                name: "FK_Vente_Article_ArticleidA",
                table: "Vente",
                column: "ArticleidA",
                principalTable: "Article",
                principalColumn: "idA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vente_Tickets_TicketIdT",
                table: "Vente",
                column: "TicketIdT",
                principalTable: "Tickets",
                principalColumn: "IdT");
        }
    }
}
