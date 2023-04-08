using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class DisplayWord
	{
        public int ID { get; set; }

        //foreign key
        //many DisplayWord to one Word
        public int WordId { get; set; }
        public Word Word { get; set; }

        //unic key
        [Required(ErrorMessage = "The display field is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The display must be between 2 and 50 characters.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "The display can only contain letters.")]
        [Remote("IsDisplayUnique", "DisplaySentence", AdditionalFields = "ID", ErrorMessage = "The display is not unique.")]
        public string Display { get; set; }
    }
}

