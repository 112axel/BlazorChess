using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorChess.Migrations
{
    /// <inheritdoc />
    public partial class promotePice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PromotionChoise",
                table: "HistoryMove",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PromotionChoise",
                table: "HistoryMove");
        }
    }
}
