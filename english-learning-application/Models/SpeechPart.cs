using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class SpeechPart
	{
        public int ID { get; set; }

        //unic key
        [Required(ErrorMessage = "The name field is required.")]
        [MaxLength(50, ErrorMessage = "The name field cannot be longer than 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The name field can only contain letters.")]
        [Remote("IsNameUnique", "SpeechPart", AdditionalFields = "ID", ErrorMessage = "The name is already taken.")]
        public string Name { get; set; }

        //one SpeechPart to many TranslatedWord
        public List<TranslatedWord> TranslatedWords { get; set; }

        public SpeechPart()
        {
            TranslatedWords = new List<TranslatedWord>();
        }
    }
}

