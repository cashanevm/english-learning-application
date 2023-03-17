using System;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class SpeechPart
	{
        public int ID { get; set; }

        //unic key
        public string Name { get; set; }

        //one SpeechPart to many TranslatedWord
        public List<TranslatedWord> TranslatedWords { get; set; }
    }
}

