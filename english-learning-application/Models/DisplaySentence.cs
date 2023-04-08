using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class DisplaySentence
	{
        public int ID { get; set; }

        //foreign key
        //many DisplaySentence to one Sentence
        public int SentenceId { get; set; }
        public Sentence Sentence { get; set; }

        //unic key
        [Required(ErrorMessage = "The display is required.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "The display must be between 5 and 255 characters.")]
        [RegularExpression("^[a-zA-Z0-9\\s]*$", ErrorMessage = "The display can only contain letters, numbers, and spaces.")]
        [Remote("IsDisplayUnique", "DisplaySentence", AdditionalFields = "ID", ErrorMessage = "The display is not unique.")]
        public string Display { get; set; }
    }
}

