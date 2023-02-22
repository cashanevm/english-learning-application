using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class Sentence
	{
        //public Sentence()
        //{
        //          //ContextIds = new ArrayList();
        //      }

        //public Sentence(int id, ArrayList contextIds, string sentence, int ownerId, string word, string originalSentence)
        //{
        //          this.ID = id;
        //	this.ContextIds = contextIds;
        //	this.SentenceDisplay = sentence;
        //	this.OwnerId = ownerId;
        //	this.Word = word;
        //	this.OriginalSentence = originalSentence;
        //}

        public int ID { get; set; }
		public List<Context> Contexts { get; set; }
		public string SentenceDisplay { get; set; }
		public int OwnerId { get; set; }
		public string Word { get; set; }
		public string OriginalSentence { get; set; }

	}
}

