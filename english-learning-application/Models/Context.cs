using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class Context
    {
        public int ID { get; set; }

        //unic key
        [Required(ErrorMessage = "The name is required.")]
        [MaxLength(100, ErrorMessage = "The name cannot be longer than 100 characters.")]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "The display can only contain letters, numbers, and spaces.")]
        [Remote("IsNameUnique", "Context", AdditionalFields = "ID", ErrorMessage = "The name is not unique.")]
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

