using System;
namespace english_learning_application.Models
{
	public class Word
	{
		public Word()
		{
		}

		public int Id { get; set; }

        public string TagIds { get; set; }

        public string WordDisplay { get; set; }

        public string OriginalWord { get; set; }
    }
}

