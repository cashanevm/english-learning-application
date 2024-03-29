using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class TranslatedSentence
	{
        public int ID { get; set; }

        //many TranslatedSentence to many Context
        public List<Context> Contexts { get; set; }

        [Required(ErrorMessage = "The owner ID is required.")]
        public int OwnerId { get; set; }

        //foreign key
        //many TranslatedSentence to one Word
        public int WordId { get; set; }
        public Word Word { get; set; }

        //foreign key
        //many TranslatedSentence to one Sentence
        public int SentenceId { get; set; }
        public Sentence Sentence { get; set; }

        //foreign key
        //many TranslatedSentence to one Language
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        //unic key
        [Required(ErrorMessage = "The translation is required.")]
        [Remote("IsTranslationUnique", "TranslatedSentences", AdditionalFields = "ID", ErrorMessage = "The translation is not unique.")]
        [StringLength(255, ErrorMessage = "The translation cannot exceed 255 characters.")]
        public string Translation { get; set; }

        public TranslatedSentence()
        {
            Contexts = new List<Context>();
        }
    }
}

