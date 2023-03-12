using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID), nameof(Name))]
    public class Tag
	{
		
        public int ID { get; set; }

        //unic key
        public string Name { get; set; }

        //many Tag to many Word
        public List<Word> Words { get; set; }
    }
}

