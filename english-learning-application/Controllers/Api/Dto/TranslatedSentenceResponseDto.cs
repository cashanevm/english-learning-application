using System;
using english_learning_application.Models;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers.Api.Dto
{
	public class TranslatedSentenceResponseDto
	{
		public TranslatedSentenceResponseDto(TranslatedSentence translatedSentence)
		{

            ID = translatedSentence.ID;
            OwnerId = translatedSentence.OwnerId;
            WordId = translatedSentence.WordId;
            SentenceId = translatedSentence.SentenceId;
            LanguageId = translatedSentence.LanguageId;
            Translation = translatedSentence.Translation;

            ContextIds = translatedSentence.Contexts.Select(x => x.ID).ToList();
        }

        public int ID { get; set; }

        //many TranslatedSentence to many Context
        public List<int> ContextIds { get; set; }

        public int OwnerId { get; set; }

        //foreign key
        //many TranslatedSentence to one Word
        public int WordId { get; set; }

        //foreign key
        //many TranslatedSentence to one Sentence
        public int SentenceId { get; set; }

        //foreign key
        //many TranslatedSentence to one Language
        public int LanguageId { get; set; }

        //unic key
        public string Translation { get; set; }
    }
}

