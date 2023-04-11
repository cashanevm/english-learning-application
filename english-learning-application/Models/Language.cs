using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class Language
	{
        public int ID { get; set; }

        //unic key
        [Required]
        public string Key { get; set; }

        //one Language to many Test
        public List<Test> Tests { get; set; }

        //one Language to many TranslatedSentence
        public List<TranslatedSentence> TranslatedSentences { get; set; }

        //one Language to many TranslatedWord
        public List<TranslatedWord> TranslatedWords { get; set; }


        public Language()
        {
            Tests = new List<Test>();
            TranslatedSentences = new List<TranslatedSentence>();
            TranslatedWords = new List<TranslatedWord>();
        }
    }
}

