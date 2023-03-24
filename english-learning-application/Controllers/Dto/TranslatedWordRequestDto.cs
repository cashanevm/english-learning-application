using System;
using english_learning_application.Models;

namespace english_learning_application.Controllers.Dto
{
	public class TranslatedWordRequestDto
	{
		public TranslatedWordRequestDto()
        {
            ContextIds = new List<int>();
        }

        //many TranslatedWord to many Contexts
        public List<int> ContextIds { get; set; }

        public int OwnerId { get; set; }

        //foreign key
        //many TranslatedWord to one Word
        public int WordId { get; set; }

        //unic key
        public string Translation { get; set; }

        //foreign key
        //many TranslatedWord to one Language
        public int LanguageId { get; set; }

        public int SpeechPartId { get; set; }
    }
}

