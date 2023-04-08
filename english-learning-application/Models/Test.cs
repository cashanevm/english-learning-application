using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class Test
	{

        public int ID { get; set; }

        [Required(ErrorMessage = "Time per word is required.")]
        [Range(1, 60, ErrorMessage = "Time per word must be between 1 and 60 seconds.")]
        public int TimePerWord { get; set; }

        [Required(ErrorMessage = "Owner ID is required.")]
        public int OwnerId { get; set; }

        [Required(ErrorMessage = "Number of options is required.")]
        [Range(2, 10, ErrorMessage = "Number of options must be between 2 and 10.")]
        public int Options { get; set; }

        //many Test to many Word
        public List<Word> Words { get; set; }

        //foreign key
        //many Test to one Language
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public Test()
        {
            Words = new List<Word>();
        }
    }
}

