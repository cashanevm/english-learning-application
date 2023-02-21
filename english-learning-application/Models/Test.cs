using System;
using System.Collections;

namespace english_learning_application.Models
{
	public class Test
	{
		public Test()
		{
            WordsIds = new ArrayList();
        }

        public int Id { get; set; }

        public int TimePerWord { get; set; }

        public int OwnerId { get; set; }

        public int Options { get; set; }

        public ArrayList WordsIds { get; set; }

        public string Language { get; set; }


    }
}

