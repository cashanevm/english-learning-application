using System;
using english_learning_application.Models;

namespace english_learning_application.Controllers.Api.Dto
{
	public class ContextResponseDto
	{
		public ContextResponseDto(Context context)
		{
            ID = context.ID;
            Name = context.Name;
            TranslatedWordIds = context.TranslatedWords.Select(x => x.ID).ToList();
            TranslatedSentenceIds = context.TranslatedSentences.Select(x => x.ID).ToList();
            SentenceIds = context.Sentences.Select(x => x.ID).ToList();

        }

        public int ID { get; set; }

        
        public string Name { get; set; }

        
        public List<int> TranslatedWordIds { get; set; }

       
        public List<int> TranslatedSentenceIds { get; set; }

        
        public List<int> SentenceIds { get; set; }
    }
}

