using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class SpeechPart
	{
        public int ID { get; set; }

        //unic key
        [Required]
        public string Name { get; set; }

        //one SpeechPart to many TranslatedWord
        public List<TranslatedWord> TranslatedWords { get; set; }

        public SpeechPart()
        {
            TranslatedWords = new List<TranslatedWord>();
        }
    }
}

