using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork3_DB.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Users_UserId",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Groups_groupId",
                table: "Models");

            migrationBuilder.RenameColumn(
                name: "groupId",
                table: "Models",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Models_groupId",
                table: "Models",
                newName: "IX_Models_GroupId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Models",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Groups",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Groups_GroupId",
                table: "Models",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Users_UserId",
                table: "Models",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Groups_GroupId",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Users_UserId",
                table: "Models");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Models",
                newName: "groupId");

            migrationBuilder.RenameIndex(
                name: "IX_Models_GroupId",
                table: "Models",
                newName: "IX_Models_groupId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Models",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Users_UserId",
                table: "Models",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Groups_groupId",
                table: "Models",
                column: "groupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
