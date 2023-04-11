using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class Word
	{
        public int ID { get; set; }

        //many Word to many Tag
        public List<Tag> Tags { get; set; }

        //one Word to many DisplayWord -
        public List<DisplayWord> DisplayWords { get; set; }

        //one Word to many Sentence -
        public List<Sentence> Sentences { get; set; }

        // one Word to many TranslatedSentence -
        public List<TranslatedSentence> TranslatedSentences { get; set; }

        // one Word to many TranslatedWord -
        public List<TranslatedWord> TranslatedWords { get; set; }

        //many Word to many Test -
        public List<Test> Tests { get; set; }

        //unic key
        [Required(ErrorMessage = "The original word is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "The original word must be between 1 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The original word can only contain letters.")]
        [Remote("IsOriginalWordUnique", "Word", AdditionalFields = "ID", ErrorMessage = "The original word is not unique.")]
        public string OriginalWord { get; set; }

        public Word()
        {
            Tags = new List<Tag>();
            DisplayWords = new List<DisplayWord>();
            Sentences = new List<Sentence>();
            TranslatedSentences = new List<TranslatedSentence>();
            TranslatedWords = new List<TranslatedWord>();
            Tests = new List<Test>();
        }
    }
}

