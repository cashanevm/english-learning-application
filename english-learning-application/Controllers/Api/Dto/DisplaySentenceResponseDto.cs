using System;
using english_learning_application.Models;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers.Api.Dto
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

        public int SentenceId { get; set; }

        public string Display { get; set; }
    }
}

