using System;
using english_learning_application.Models;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers.Dto
{
	public class DisplaySentenceResponseDto
	{
		public DisplaySentenceResponseDto(DisplaySentence displaySentence)
		{
            ID = displaySentence.ID;
            SentenceId = displaySentence.SentenceId;
            Display = displaySentence.Display;
        }

        public int ID { get; set; }

        //foreign key
        //many DisplaySentence to one Sentence
        public int SentenceId { get; set; }

        //unic key
        public string Display { get; set; }
    }
}

