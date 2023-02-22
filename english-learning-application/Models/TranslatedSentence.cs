using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class TranslatedSentence
	{
		//public TranslatedSentence()
		//{
  //          //this.ContextIds = new ArrayList();
  //      }

        //public TranslatedSentence(int id, ArrayList contextIds, int ownerId, string word, string translSentence, string translLanguage)
        //{
        //    this.ID = id;
        //    this.ContextIds = contextIds;
        //    this.OwnerId = ownerId;
        //    this.Word = word;
        //    this.TranslSentence = translSentence;
        //    this.TransLanguage = translLanguage;
        //}

        public int ID { get; set; }
        public List<Context> Contexts { get; set; }
        public int OwnerId { get; set; }
        public string Word { get; set; }
        public string TranslSentence { get; set; }
        public string TransLanguage { get; set; }

    }
}

