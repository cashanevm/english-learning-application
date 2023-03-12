using System;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class Language
	{
        public int ID { get; set; }

        //unic key
        public string Key { get; set; }

        //one Language to many Test
        public List<Test> Tests { get; set; }

        //one Language to many TranslatedSentence
        public List<TranslatedSentence> TranslatedSentences { get; set; }

        //one Language to many TranslatedWord
        public List<TranslatedWord> TranslatedWords { get; set; }
    }
}

