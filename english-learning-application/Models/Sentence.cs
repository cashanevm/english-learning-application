using System;
using System.Collections;

namespace english_learning_application.Models
{
	public class Sentence
	{
		public Sentence()
		{
			contextIds = new ArrayList();
        }

		public Sentence(int id, ArrayList contextIds, string sentence, int ownerId, string word, string originalSentence)
		{
			Id = id;
			this.contextIds = contextIds;
			this.sentence = sentence;
			this.ownerId = ownerId;
			this.word = word;
			this.originalSentence = originalSentence;
		}

		public int Id { get; set; }
		public ArrayList contextIds { get; set; }
		public string sentence { get; set; }
		public int ownerId { get; set; }
		public string word { get; set; }
		public string originalSentence { get; set; }

	}
}

