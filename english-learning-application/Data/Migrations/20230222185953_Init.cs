using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace english_learning_application.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sentences",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SentenceDisplay = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Word = table.Column<string>(type: "TEXT", nullable: false),
                    OriginalSentence = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sentences", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimePerWord = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Options = table.Column<int>(type: "INTEGER", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TranslatedSentences",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Word = table.Column<string>(type: "TEXT", nullable: false),
                    TranslSentence = table.Column<string>(type: "TEXT", nullable: false),
                    TransLanguage = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslatedSentences", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TranslatedWords",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Word = table.Column<string>(type: "TEXT", nullable: false),
                    Translation = table.Column<string>(type: "TEXT", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslatedWords", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false),
                    OriginalWord = table.Column<string>(type: "TEXT", nullable: false),
                    WordDisplay = table.Column<string>(type: "TEXT", nullable: false),
                    TestID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => new { x.ID, x.OriginalWord });
                    table.ForeignKey(
                        name: "FK_Words_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Contexts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SentenceID = table.Column<int>(type: "INTEGER", nullable: true),
                    TranslatedSentenceID = table.Column<int>(type: "INTEGER", nullable: true),
                    TranslatedWordID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contexts", x => new { x.ID, x.Name });
                    table.ForeignKey(
                        name: "FK_Contexts_Sentences_SentenceID",
                        column: x => x.SentenceID,
                        principalTable: "Sentences",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Contexts_TranslatedSentences_TranslatedSentenceID",
                        column: x => x.TranslatedSentenceID,
                        principalTable: "TranslatedSentences",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Contexts_TranslatedWords_TranslatedWordID",
                        column: x => x.TranslatedWordID,
                        principalTable: "TranslatedWords",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    WordID = table.Column<int>(type: "INTEGER", nullable: true),
                    WordOriginalWord = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => new { x.ID, x.Name });
                    table.ForeignKey(
                        name: "FK_Tags_Words_WordID_WordOriginalWord",
                        columns: x => new { x.WordID, x.WordOriginalWord },
                        principalTable: "Words",
                        principalColumns: new[] { "ID", "OriginalWord" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contexts_SentenceID",
                table: "Contexts",
                column: "SentenceID");

            migrationBuilder.CreateIndex(
                name: "IX_Contexts_TranslatedSentenceID",
                table: "Contexts",
                column: "TranslatedSentenceID");

            migrationBuilder.CreateIndex(
                name: "IX_Contexts_TranslatedWordID",
                table: "Contexts",
                column: "TranslatedWordID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_WordID_WordOriginalWord",
                table: "Tags",
                columns: new[] { "WordID", "WordOriginalWord" });

            migrationBuilder.CreateIndex(
                name: "IX_Words_TestID",
                table: "Words",
                column: "TestID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contexts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Sentences");

            migrationBuilder.DropTable(
                name: "TranslatedSentences");

            migrationBuilder.DropTable(
                name: "TranslatedWords");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}
