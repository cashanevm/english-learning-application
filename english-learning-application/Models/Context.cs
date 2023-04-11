using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class Context
    {
        public int ID { get; set; }

        //unic key
        [Required]
        public string Name { get; set; }

        //many Context to many TranslatedWord
        public List<TranslatedWord> TranslatedWords  { get; set; }

        //many Context to many TranslatedSentence
        public List<TranslatedSentence> TranslatedSentences { get; set; }

        //many Context to many Sentence
        public List<Sentence> Sentences { get; set; }

        public Context()
        {
            TranslatedWords = new List<TranslatedWord>();
            TranslatedSentences = new List<TranslatedSentence>();
            Sentences = new List<Sentence>();
        }
    }
    
}

