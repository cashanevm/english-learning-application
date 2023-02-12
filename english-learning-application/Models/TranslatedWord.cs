using System;
using System.Collections;

namespace english_learning_application.Models
{
	public class TranslatedWord
	{
		public TranslatedWord()
		{
            contextIds = new ArrayList();
        }

        public TranslatedWord(int id, int ownerId, string word, string translation, string language)
        {
            Id = id;
            this.OwnerId = ownerId;
            this.Word = word;
            this.Translation = translation;
            this.Language = language;
        }

        public int Id { get; set; }
        public ArrayList contextIds { get; set; }
        public int OwnerId { get; set; } 
        public string Word { get; set; }
        public string Translation { get; set; }
        public string Language { get; set; }

    }
}

