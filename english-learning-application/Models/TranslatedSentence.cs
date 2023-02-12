using System;
using System.Collections;

namespace english_learning_application.Models
{
	public class TranslatedSentence
	{
		public TranslatedSentence()
		{
            contextIds = new ArrayList();
        }

        public TranslatedSentence(int id, ArrayList contextIds, int ownerId, string word, string translSentence, string translLanguage)
        {
            Id = id;
            this.contextIds = contextIds;
            this.OwnerId = ownerId;
            this.Word = word;
            this.TranslSentence = translSentence;
            this.TransLanguage = translLanguage;
        }

        public int Id { get; set; }
        public ArrayList contextIds { get; set; }
        public int OwnerId { get; set; }
        public string Word { get; set; }
        public string TranslSentence { get; set; }
        public string TransLanguage { get; set; }

    }
}

