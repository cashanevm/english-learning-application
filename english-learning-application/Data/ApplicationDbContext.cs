﻿using System;
using System.Collections;
using System.Reflection.Metadata;
using english_learning_application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
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

        //Sentence
        //builder.Entity<Sentence>()
        //     .HasMany<Context>(sentence => sentence.Contexts)
        //     .WithMany(context => context.Sentences);

        builder.Entity<Sentence>()
            .HasMany<DisplaySentence>(sentence => sentence.DisplaySentences)
            .WithOne(displaySentence => displaySentence.Sentence)
             .HasForeignKey(displaySentence => displaySentence.SentenceId);

        builder.Entity<Sentence>()
            .HasMany<TranslatedSentence>(sentence => sentence.TranslatedSentences)
            .WithOne(translatedSentence => translatedSentence.Sentence)
            .HasForeignKey(translatedSentence => translatedSentence.SentenceId);

        builder.Entity<Sentence>()
            .HasOne<Word>(sentence => sentence.Word)
            .WithMany(word => word.Sentences)
            .HasForeignKey(sentence => sentence.WordId);

        builder.Entity<Sentence>()
           .HasIndex(sentence => sentence.OriginalSentence)
           .IsUnique();

        //Tag
        builder.Entity<Tag>()
            .HasMany<Word>(tag => tag.Words)
            .WithMany(words => words.Tags);

        builder.Entity<Tag>()
            .HasIndex(tag => tag.Name)
            .IsUnique();

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

        //Test
        builder.Entity<Test>()
         .HasMany<Word>(test => test.Words)
         .WithMany(test => test.Tests);

        builder.Entity<Test>()
            .HasOne<Language>(test => test.Language)
            .WithMany(language => language.Tests)
            .HasForeignKey(test => test.LanguageId);

        //TranslatedSentence
        //builder.Entity<TranslatedSentence>()
        //    .HasMany<Context>(translatedSentence => translatedSentence.Contexts)
        //    .WithMany(context => context.TranslatedSentences);

        builder.Entity<TranslatedSentence>()
            .HasOne<Word>(translatedSentence => translatedSentence.Word)
            .WithMany(word => word.TranslatedSentences)
            .HasForeignKey(translatedSentence => translatedSentence.WordId);

        //builder.Entity<TranslatedSentence>()
        //    .HasOne<Sentence>(translatedSentence => translatedSentence.Sentence)
        //    .WithMany(sentence => sentence.TranslatedSentences)
        //    .HasForeignKey(translatedSentence => translatedSentence.SentenceId);

        builder.Entity<TranslatedSentence>()
            .HasOne<Language>(translatedSentence => translatedSentence.Language)
            .WithMany(language => language.TranslatedSentences)
            .HasForeignKey(translatedSentence => translatedSentence.LanguageId);

        builder.Entity<TranslatedSentence>()
          .HasIndex(translatedSentence => translatedSentence.Translation)
          .IsUnique();

        //TranslatedWord
        //builder.Entity<TranslatedWord>()
        //    .HasMany<Context>(translatedWord => translatedWord.Contexts)
        //    .WithMany(context => context.TranslatedWords);



        builder.Entity<TranslatedWord>()
            .HasOne<Language>(translatedWord => translatedWord.Language)
            .WithMany(language => language.TranslatedWords)
            .HasForeignKey(translatedWord => translatedWord.LanguageId);

        builder.Entity<TranslatedWord>()
            .HasIndex(translatedWord => translatedWord.Translation)
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
