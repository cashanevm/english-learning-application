using System;
namespace english_learning_application.Controllers.Api.Dto
{
	public class SentenceRequestDto
	{
		public SentenceRequestDto()
        {
            ContextIds = new List<int>();
        }

        //many Sentence to many Context
        public List<int> ContextIds { get; set; }

        public int OwnerId { get; set; }

        //foreign key
        //many Sentence to one Word
        public int WordId { get; set; }

        //unic key
        public string OriginalSentence { get; set; }
    }
}

