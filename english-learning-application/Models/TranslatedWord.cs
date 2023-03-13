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

        //many TranslatedWord to many Contexts
        public List<Context> Contexts { get; set; }

        public int OwnerId { get; set; }

        //foreign key
        //many TranslatedWord to one Word
        public int WordId { get; set; }
        public Word Word { get; set; }

        //unic key
        public string Translation { get; set; }

        //foreign key
        //many TranslatedWord to one Language
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}

