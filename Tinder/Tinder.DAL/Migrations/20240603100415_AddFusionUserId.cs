using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tinder.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddFusionUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FusionId",
                table: "Users",
                newName: "FusionUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FusionUserId",
                table: "Users",
                newName: "FusionId");
        }
    }
}
