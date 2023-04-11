using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class Sentence
	{
        public int ID { get; set; }

        //many Sentence to many Context
        public List<Context> Contexts { get; set; }

        //many DisplaySentence to one Sentence
        public List<DisplaySentence> DisplaySentences { get; set; }

        //many TranslatedSentence to one Sentence
        public List<TranslatedSentence> TranslatedSentences { get; set; }

        public int OwnerId { get; set; }

        //foreign key
        //many Sentence to one Word
        public int WordId { get; set; }
        public Word Word { get; set; }

        //unic key
        [Required(ErrorMessage = "The original sentence field is required.")]
        [Remote("IsOriginalSentenceUnique", "Sentence", AdditionalFields = "ID", ErrorMessage = "The sentence is not unique.")]
        public string OriginalSentence { get; set; }

        // validation to ensure that the sentence contains at least one word
        [RegularExpression(@"\w+", ErrorMessage = "The sentence must contain at least one word.")]
        public string ValidateWordsInSentence => OriginalSentence;

        // validation to ensure that the sentence doesn't contain any profanity
        [RegularExpression("(?i)\\b(?!profanity1|profanity2)\\w+\\b", ErrorMessage = "The sentence cannot contain any profanity.")]
        public string ValidateNoProfanityInSentence => OriginalSentence;

        // validation to ensure that the sentence is not too long
        [StringLength(1000, ErrorMessage = "The sentence cannot be longer than 1000 characters.")]
        public string ValidateSentenceLength => OriginalSentence;

        public Sentence()
        {
            Contexts = new List<Context>();
            DisplaySentences = new List<DisplaySentence>();
            TranslatedSentences = new List<TranslatedSentence>();
        }
    }
}

