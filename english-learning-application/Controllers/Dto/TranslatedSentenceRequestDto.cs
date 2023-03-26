using System;
namespace english_learning_application.Controllers.Dto
{
	public class TranslatedSentenceRequestDto
	{
		public TranslatedSentenceRequestDto()
        {
            ContextIds = new List<int>();
        }
      
        public List<int> ContextIds { get; set; }

        public int OwnerId { get; set; }

        public int WordId { get; set; }

        public int SentenceId { get; set; }

        public int LanguageId { get; set; }

        public string Translation { get; set; }
    }
}

