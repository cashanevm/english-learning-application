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

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

