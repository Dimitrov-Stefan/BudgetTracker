using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddedUserRelationsToFinancialEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialOperation_FinancialItems_FinancialItemId",
                table: "FinancialOperation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialOperation",
                table: "FinancialOperation");

            migrationBuilder.RenameTable(
                name: "FinancialOperation",
                newName: "FinancialOperations");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialOperation_FinancialItemId",
                table: "FinancialOperations",
                newName: "IX_FinancialOperations_FinancialItemId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FinancialItems",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FinancialItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialOperations",
                table: "FinancialOperations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialItems_UserId",
                table: "FinancialItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialItems_Users_UserId",
                table: "FinancialItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialOperations_FinancialItems_FinancialItemId",
                table: "FinancialOperations",
                column: "FinancialItemId",
                principalTable: "FinancialItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialItems_Users_UserId",
                table: "FinancialItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialOperations_FinancialItems_FinancialItemId",
                table: "FinancialOperations");

            migrationBuilder.DropIndex(
                name: "IX_FinancialItems_UserId",
                table: "FinancialItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialOperations",
                table: "FinancialOperations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FinancialItems");

            migrationBuilder.RenameTable(
                name: "FinancialOperations",
                newName: "FinancialOperation");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialOperations_FinancialItemId",
                table: "FinancialOperation",
                newName: "IX_FinancialOperation_FinancialItemId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FinancialItems",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialOperation",
                table: "FinancialOperation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialOperation_FinancialItems_FinancialItemId",
                table: "FinancialOperation",
                column: "FinancialItemId",
                principalTable: "FinancialItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
