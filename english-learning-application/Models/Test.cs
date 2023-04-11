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

        [Required]
        public int TimePerWord { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
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

