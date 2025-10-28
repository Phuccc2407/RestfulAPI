using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulAPI.Migrations
{
    /// <inheritdoc />
    public partial class imageupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackImageUrl",
                table: "Tracks",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArtistImageUrl",
                table: "Artists",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackImageUrl",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "ArtistImageUrl",
                table: "Artists");
        }
    }
}
