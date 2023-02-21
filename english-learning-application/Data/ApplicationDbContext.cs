using System;
using english_learning_application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Context> Contexts { get; set; }

        public DbSet<Sentence> Sentences { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Test> tests { get; set; }

        public DbSet<TranslatedSentence> TranslatedSentences { get; set; }

        public DbSet<TranslatedWord> TranslatedWords { get; set; }

        public DbSet<Word> Words { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)

        {

        }
    }
}

