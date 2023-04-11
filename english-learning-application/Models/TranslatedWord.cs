using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class TranslatedWord
	{
        public int ID { get; set; }

        //many TranslatedWord to many Contexts
        public List<Context> Contexts { get; set; }

        [Required(ErrorMessage = "The owner ID is required.")]
        public int OwnerId { get; set; }

        //foreign key
        //many TranslatedWord to one Word
        public int WordId { get; set; }
        public Word Word { get; set; }

        //unic key
        [Required(ErrorMessage = "The translation is required.")]
        [Remote("IsTranslationUnique", "TranslatedWords", AdditionalFields = "ID", ErrorMessage = "The translation is not unique.")]
        [StringLength(50, ErrorMessage = "The translation must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Translation { get; set; }

        //foreign key
        //many TranslatedWord to one Language
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public int SpeechPartId { get; set; }
        public SpeechPart SpeechPart { get; set; }

        public TranslatedWord()
        {
            Contexts = new List<Context>();
        }
    }
}

