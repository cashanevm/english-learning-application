using System;
using english_learning_application.Models;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers.Dto
{
	public class SentenceResponseDto
	{
		public SentenceResponseDto(Sentence sentence)
		{
            ID = sentence.ID;
            OwnerId = sentence.OwnerId;
            WordId = sentence.WordId;
            OriginalSentence = sentence.OriginalSentence;

            ContextIds = sentence.Contexts.Select(x => x.ID).ToList();
            DisplaySentenceIds = sentence.DisplaySentences.Select(x => x.ID).ToList();
            TranslatedSentenceIds = sentence.TranslatedSentences.Select(x => x.ID).ToList();

        }

        public int ID { get; set; }

        //many Sentence to many Context
        public List<int> ContextIds { get; set; }

        //many DisplaySentence to one Sentence
        public List<int> DisplaySentenceIds { get; set; }

        //many TranslatedSentence to one Sentence
        public List<int> TranslatedSentenceIds { get; set; }

        public int OwnerId { get; set; }

        //foreign key
        //many Sentence to one Word
        public int WordId { get; set; }

        //unic key
        public string OriginalSentence { get; set; }
    }
}

