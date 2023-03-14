using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace english_learning_application.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contexts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contexts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OriginalWord = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.ID);
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
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tests_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisplayWords",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WordId = table.Column<int>(type: "INTEGER", nullable: false),
                    Display = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplayWords", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DisplayWords_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sentences",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    WordId = table.Column<int>(type: "INTEGER", nullable: false),
                    OriginalSentence = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sentences", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sentences_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagWord",
                columns: table => new
                {
                    TagsID = table.Column<int>(type: "INTEGER", nullable: false),
                    WordsID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagWord", x => new { x.TagsID, x.WordsID });
                    table.ForeignKey(
                        name: "FK_TagWord_Tags_TagsID",
                        column: x => x.TagsID,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagWord_Words_WordsID",
                        column: x => x.WordsID,
                        principalTable: "Words",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranslatedWords",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    WordId = table.Column<int>(type: "INTEGER", nullable: false),
                    Translation = table.Column<string>(type: "TEXT", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslatedWords", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TranslatedWords_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TranslatedWords_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestWord",
                columns: table => new
                {
                    TestsID = table.Column<int>(type: "INTEGER", nullable: false),
                    WordsID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestWord", x => new { x.TestsID, x.WordsID });
                    table.ForeignKey(
                        name: "FK_TestWord_Tests_TestsID",
                        column: x => x.TestsID,
                        principalTable: "Tests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestWord_Words_WordsID",
                        column: x => x.WordsID,
                        principalTable: "Words",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContextSentence",
                columns: table => new
                {
                    ContextsID = table.Column<int>(type: "INTEGER", nullable: false),
                    SentencesID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextSentence", x => new { x.ContextsID, x.SentencesID });
                    table.ForeignKey(
                        name: "FK_ContextSentence_Contexts_ContextsID",
                        column: x => x.ContextsID,
                        principalTable: "Contexts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContextSentence_Sentences_SentencesID",
                        column: x => x.SentencesID,
                        principalTable: "Sentences",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisplaySentences",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SentenceId = table.Column<int>(type: "INTEGER", nullable: false),
                    Display = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplaySentences", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DisplaySentences_Sentences_SentenceId",
                        column: x => x.SentenceId,
                        principalTable: "Sentences",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranslatedSentences",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    WordId = table.Column<int>(type: "INTEGER", nullable: false),
                    SentenceId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    Translation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslatedSentences", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TranslatedSentences_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TranslatedSentences_Sentences_SentenceId",
                        column: x => x.SentenceId,
                        principalTable: "Sentences",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TranslatedSentences_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContextTranslatedWord",
                columns: table => new
                {
                    ContextsID = table.Column<int>(type: "INTEGER", nullable: false),
                    TranslatedWordsID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextTranslatedWord", x => new { x.ContextsID, x.TranslatedWordsID });
                    table.ForeignKey(
                        name: "FK_ContextTranslatedWord_Contexts_ContextsID",
                        column: x => x.ContextsID,
                        principalTable: "Contexts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContextTranslatedWord_TranslatedWords_TranslatedWordsID",
                        column: x => x.TranslatedWordsID,
                        principalTable: "TranslatedWords",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContextTranslatedSentence",
                columns: table => new
                {
                    ContextsID = table.Column<int>(type: "INTEGER", nullable: false),
                    TranslatedSentencesID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextTranslatedSentence", x => new { x.ContextsID, x.TranslatedSentencesID });
                    table.ForeignKey(
                        name: "FK_ContextTranslatedSentence_Contexts_ContextsID",
                        column: x => x.ContextsID,
                        principalTable: "Contexts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContextTranslatedSentence_TranslatedSentences_TranslatedSentencesID",
                        column: x => x.TranslatedSentencesID,
                        principalTable: "TranslatedSentences",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contexts_Name",
                table: "Contexts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContextSentence_SentencesID",
                table: "ContextSentence",
                column: "SentencesID");

            migrationBuilder.CreateIndex(
                name: "IX_ContextTranslatedSentence_TranslatedSentencesID",
                table: "ContextTranslatedSentence",
                column: "TranslatedSentencesID");

            migrationBuilder.CreateIndex(
                name: "IX_ContextTranslatedWord_TranslatedWordsID",
                table: "ContextTranslatedWord",
                column: "TranslatedWordsID");

            migrationBuilder.CreateIndex(
                name: "IX_DisplaySentences_Display",
                table: "DisplaySentences",
                column: "Display",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisplaySentences_SentenceId",
                table: "DisplaySentences",
                column: "SentenceId");

            migrationBuilder.CreateIndex(
                name: "IX_DisplayWords_Display",
                table: "DisplayWords",
                column: "Display",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisplayWords_WordId",
                table: "DisplayWords",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Key",
                table: "Languages",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sentences_OriginalSentence",
                table: "Sentences",
                column: "OriginalSentence",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sentences_WordId",
                table: "Sentences",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagWord_WordsID",
                table: "TagWord",
                column: "WordsID");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_LanguageId",
                table: "Tests",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TestWord_WordsID",
                table: "TestWord",
                column: "WordsID");

            migrationBuilder.CreateIndex(
                name: "IX_TranslatedSentences_LanguageId",
                table: "TranslatedSentences",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TranslatedSentences_SentenceId",
                table: "TranslatedSentences",
                column: "SentenceId");

            migrationBuilder.CreateIndex(
                name: "IX_TranslatedSentences_Translation",
                table: "TranslatedSentences",
                column: "Translation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranslatedSentences_WordId",
                table: "TranslatedSentences",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_TranslatedWords_LanguageId",
                table: "TranslatedWords",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TranslatedWords_Translation",
                table: "TranslatedWords",
                column: "Translation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranslatedWords_WordId",
                table: "TranslatedWords",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_OriginalWord",
                table: "Words",
                column: "OriginalWord",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContextSentence");

            migrationBuilder.DropTable(
                name: "ContextTranslatedSentence");

            migrationBuilder.DropTable(
                name: "ContextTranslatedWord");

            migrationBuilder.DropTable(
                name: "DisplaySentences");

            migrationBuilder.DropTable(
                name: "DisplayWords");

            migrationBuilder.DropTable(
                name: "TagWord");

            migrationBuilder.DropTable(
                name: "TestWord");

            migrationBuilder.DropTable(
                name: "TranslatedSentences");

            migrationBuilder.DropTable(
                name: "Contexts");

            migrationBuilder.DropTable(
                name: "TranslatedWords");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Sentences");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
