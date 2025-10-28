using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulAPI.Migrations
{
    /// <inheritdoc />
    public partial class tracksupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__TrackGenr__Genre__71D1E811",
                table: "TrackGenres");

            migrationBuilder.DropForeignKey(
                name: "FK__TrackGenr__Track__70DDC3D8",
                table: "TrackGenres");

            migrationBuilder.DropForeignKey(
                name: "FK__Tracks__ArtistId__6E01572D",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_ArtistId",
                table: "Tracks");

            migrationBuilder.DropPrimaryKey(
                name: "PK__TrackGen__8A4CA8B797FA578A",
                table: "TrackGenres");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Tracks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackGenres",
                table: "TrackGenres",
                columns: new[] { "TrackId", "GenreId" });

            migrationBuilder.CreateTable(
                name: "TrackArtists",
                columns: table => new
                {
                    TrackId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArtistId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackArtists", x => new { x.TrackId, x.ArtistId });
                    table.ForeignKey(
                        name: "FK_TrackArtists_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "ArtistId");
                    table.ForeignKey(
                        name: "FK_TrackArtists_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackArtists_ArtistId",
                table: "TrackArtists",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackGenres_Genres_GenreId",
                table: "TrackGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackGenres_Tracks_TrackId",
                table: "TrackGenres",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "TrackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackGenres_Genres_GenreId",
                table: "TrackGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackGenres_Tracks_TrackId",
                table: "TrackGenres");

            migrationBuilder.DropTable(
                name: "TrackArtists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackGenres",
                table: "TrackGenres");

            migrationBuilder.AddColumn<string>(
                name: "ArtistId",
                table: "Tracks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK__TrackGen__8A4CA8B797FA578A",
                table: "TrackGenres",
                columns: new[] { "TrackId", "GenreId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_ArtistId",
                table: "Tracks",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK__TrackGenr__Genre__71D1E811",
                table: "TrackGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK__TrackGenr__Track__70DDC3D8",
                table: "TrackGenres",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK__Tracks__ArtistId__6E01572D",
                table: "Tracks",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId");
        }
    }
}
