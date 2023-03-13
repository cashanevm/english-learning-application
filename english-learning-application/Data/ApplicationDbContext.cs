using System;
using System.Reflection.Metadata;
using english_learning_application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.Mime.MediaTypeNames;

namespace english_learning_application.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Context> Contexts { get; set; }

    public DbSet<Sentence> Sentences { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Test> Tests { get; set; }

    public DbSet<TranslatedSentence> TranslatedSentences { get; set; }

    public DbSet<TranslatedWord> TranslatedWords { get; set; }

    public DbSet<Word> Words { get; set; }

    public DbSet<DisplayWord> DisplayWords { get; set; }

    public DbSet<DisplaySentence> DisplaySentences { get; set; }

    public DbSet<Language> Languages { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //Context
        builder.Entity<Context>()
            .HasMany<TranslatedWord>(context => context.TranslatedWords)
            .WithMany(translatedWord => translatedWord.Contexts);

        builder.Entity<Context>()
           .HasMany<TranslatedSentence>(context => context.TranslatedSentences)
           .WithMany(translatedWord => translatedWord.Contexts);

        builder.Entity<Context>()
            .HasMany<Sentence>(context => context.Sentences)
            .WithMany(translatedWord => translatedWord.Contexts);

        builder.Entity<Context>()
            .HasIndex(context => context.Name)
            .IsUnique();
        //Word
        builder.Entity<Word>()
         .HasMany<DisplayWord>(word => word.DisplayWords)
         .WithOne(displayWord => displayWord.Word)
         .HasForeignKey(displayWord => displayWord.WordId);

        builder.Entity<Word>()
            .HasMany<TranslatedWord>(word => word.TranslatedWords)
             .WithOne(translatedWord => translatedWord.Word)
         .HasForeignKey(translatedWord => translatedWord.WordId);


        builder.Entity<Word>()
            .HasIndex(word => word.OriginalWord)
            .IsUnique();

        //DisplayWord
        builder.Entity<DisplayWord>()
            .HasIndex(displayWord => displayWord.Display)
             .IsUnique();

        //DisplaySentence
        builder.Entity<DisplaySentence>()
            .HasIndex(displaySentence => displaySentence.Display)
            .IsUnique();

        //Language
        builder.Entity<Language>()
         .HasIndex(language => language.Key)
         .IsUnique();

    }
}

