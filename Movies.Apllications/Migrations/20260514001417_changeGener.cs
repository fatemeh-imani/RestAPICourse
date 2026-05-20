using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Applications.Migrations
{
    /// <inheritdoc />
    public partial class changeGener : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Geners_GenresId",
                table: "GenreMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Geners",
                table: "Geners");

            migrationBuilder.RenameTable(
                name: "Geners",
                newName: "Genres");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Genres_GenresId",
                table: "GenreMovie",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Genres_GenresId",
                table: "GenreMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Geners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Geners",
                table: "Geners",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Geners_GenresId",
                table: "GenreMovie",
                column: "GenresId",
                principalTable: "Geners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
