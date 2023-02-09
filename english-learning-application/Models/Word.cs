using Microsoft.AspNetCore.Server.IIS.Core;
using System;
using System.Collections;

namespace english_learning_application.Models
{
	public class Word
	{
		public Word()
		{
			TagIds = new ArrayList();
		}

		public Word(int id, ArrayList TagIds, string WordDisplay, string OriginalWord)
		{
			this.Id = id;
			this.TagIds = TagIds;
			this.WordDisplay = WordDisplay;	
			this.OriginalWord = OriginalWord;
		}

		public int Id { get; set; }

        public ArrayList TagIds { get; set; }

        public string WordDisplay { get; set; }

        public string OriginalWord { get; set; }
    }
}

