using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestAPI.Migrations
{
    /// <inheritdoc />
    public partial class dto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Subastas_ProductoId",
                table: "Pujas");

            migrationBuilder.DropIndex(
                name: "IX_Pujas_ProductoId",
                table: "Pujas");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Pujas");

            migrationBuilder.AddColumn<int>(
                name: "SubastasEntityId",
                table: "Pujas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_SubastasEntityId",
                table: "Pujas",
                column: "SubastasEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pujas_Subastas_SubastasEntityId",
                table: "Pujas",
                column: "SubastasEntityId",
                principalTable: "Subastas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pujas_Subastas_SubastasEntityId",
                table: "Pujas");

            migrationBuilder.DropIndex(
                name: "IX_Pujas_SubastasEntityId",
                table: "Pujas");

            migrationBuilder.DropColumn(
                name: "SubastasEntityId",
                table: "Pujas");

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Pujas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pujas_ProductoId",
                table: "Pujas",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pujas_Subastas_ProductoId",
                table: "Pujas",
                column: "ProductoId",
                principalTable: "Subastas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
