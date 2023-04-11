using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class Tag
	{
		
        public int ID { get; set; }

        //unic key
        [Required]
        public string Name { get; set; }

        //many Tag to many Word
        public List<Word> Words { get; set; }

         public Tag()
        {
            Words = new List<Word>();
        }
    }
}

