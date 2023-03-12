using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class TranslatedWord
	{
        public int ID { get; set; }

        public List<Context> Contexts { get; set; }

        public int OwnerId { get; set; } 

        public string Word { get; set; }

        public string Translation { get; set; }

        public string Language { get; set; }
    }
}

