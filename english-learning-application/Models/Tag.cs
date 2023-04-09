using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class Tag
	{
		
        public int ID { get; set; }

        //unic key
        [Required(ErrorMessage = "The name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 50 characters long.")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "The name can only contain letters, numbers, and spaces.")]
        [Remote("IsNameUnique", "Tag", AdditionalFields = "ID", ErrorMessage = "The name is not unique.")]
        public string Name { get; set; }

        //many Tag to many Word
        public List<Word> Words { get; set; }

         public Tag()
        {
            Words = new List<Word>();
        }
    }
}

