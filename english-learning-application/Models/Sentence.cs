using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string OriginalSentence { get; set; }

        public Sentence()
        {
            Contexts = new List<Context>();
            DisplaySentences = new List<DisplaySentence>();
            TranslatedSentences = new List<TranslatedSentence>();
        }
    }
}

