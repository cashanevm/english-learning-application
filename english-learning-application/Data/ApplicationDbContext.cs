﻿using System;
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
		  .HasIndex(translatedSentence => translatedSentence.Sentence)
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
	}
}

