using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class upisaneGodine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UpisaneGodine",
                columns: table => new
                {
                    GodinaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GodinaStudija = table.Column<int>(type: "int", nullable: false),
                    DatumUpisa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AkGodinaId = table.Column<int>(type: "int", nullable: false),
                    CijenaSkolarine = table.Column<float>(type: "real", nullable: false),
                    Obnova = table.Column<bool>(type: "bit", nullable: false),
                    DatumOvjere = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Napomena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvidentiraoKorisnikId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpisaneGodine", x => x.GodinaId);
                    table.ForeignKey(
                        name: "FK_UpisaneGodine_AkademskaGodina_AkGodinaId",
                        column: x => x.AkGodinaId,
                        principalTable: "AkademskaGodina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpisaneGodine_KorisnickiNalog_EvidentiraoKorisnikId",
                        column: x => x.EvidentiraoKorisnikId,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpisaneGodine_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpisaneGodine_AkGodinaId",
                table: "UpisaneGodine",
                column: "AkGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisaneGodine_EvidentiraoKorisnikId",
                table: "UpisaneGodine",
                column: "EvidentiraoKorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisaneGodine_StudentId",
                table: "UpisaneGodine",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpisaneGodine");
        }
    }
}
