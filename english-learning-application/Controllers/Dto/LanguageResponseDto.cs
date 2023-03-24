using System;
using english_learning_application.Models;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers.Dto
{
	public class LanguageResponseDto
	{
		public LanguageResponseDto(Language language)
		{
            ID = language.ID;
            Key = language.Key;

            TestIds = language.Tests.Select(x => x.ID).ToList();
            TranslatedSentenceIds = language.TranslatedSentences.Select(x => x.ID).ToList();
            TranslatedWordIds = language.TranslatedWords.Select(x => x.ID).ToList();

        }

        public int ID { get; set; }

        //unic key
        public string Key { get; set; }

        //one Language to many Test
        public List<int> TestIds { get; set; }

        //one Language to many TranslatedSentence
        public List<int> TranslatedSentenceIds { get; set; }

        //one Language to many TranslatedWord
        public List<int> TranslatedWordIds { get; set; }
    }
}

