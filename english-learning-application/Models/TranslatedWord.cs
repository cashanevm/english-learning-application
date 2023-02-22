using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class TranslatedWord
	{
		//public TranslatedWord()
		//{
  //          //ContextIds = new ArrayList();
  //      }

        //public TranslatedWord(int id, int ownerId, string word, string translation, string language)
        //{
        //    this.ID = id;
        //    this.OwnerId = ownerId;
        //    this.Word = word;
        //    this.Translation = translation;
        //    this.Language = language;
        //}

        public int ID { get; set; }
        public List<Context> Contexts { get; set; }
        public int OwnerId { get; set; } 
        public string Word { get; set; }
        public string Translation { get; set; }
        public string Language { get; set; }

    }
}

