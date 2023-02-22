using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID), nameof(OriginalWord))]
    public class Word
	{
		//public Word()
		//{
  //          //this.TagIds = new ArrayList();
		//}

		//public Word(int id, ArrayList TagIds, string WordDisplay, string OriginalWord)
		//{
		//	this.ID = id;
		//	this.TagIds = TagIds;
		//	this.WordDisplay = WordDisplay;	
		//	this.OriginalWord = OriginalWord;
		//}

        public int ID { get; set; }

        public List<Tag> Tags { get; set; }

        public string WordDisplay { get; set; }

        public string OriginalWord { get; set; }
    }
}

