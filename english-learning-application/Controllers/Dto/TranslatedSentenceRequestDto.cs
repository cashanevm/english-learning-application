using System;
namespace english_learning_application.Controllers.Dto
{
	public class TranslatedSentenceRequestDto
	{
		public TranslatedSentenceRequestDto()
        {
            ContextIds = new List<int>();
        }

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

