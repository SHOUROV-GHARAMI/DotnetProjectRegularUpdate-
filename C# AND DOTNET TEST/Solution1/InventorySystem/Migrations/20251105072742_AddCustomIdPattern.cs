using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventorySystem.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomIdPattern : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_AspNetUsers_OwnerId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryAccesses_AspNetUsers_UserId",
                table: "InventoryAccesses");

            migrationBuilder.AddColumn<string>(
                name: "CustomIdPattern",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_AspNetUsers_OwnerId",
                table: "Inventories",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryAccesses_AspNetUsers_UserId",
                table: "InventoryAccesses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_AspNetUsers_OwnerId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryAccesses_AspNetUsers_UserId",
                table: "InventoryAccesses");

            migrationBuilder.DropColumn(
                name: "CustomIdPattern",
                table: "Inventories");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_AspNetUsers_OwnerId",
                table: "Inventories",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryAccesses_AspNetUsers_UserId",
                table: "InventoryAccesses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
