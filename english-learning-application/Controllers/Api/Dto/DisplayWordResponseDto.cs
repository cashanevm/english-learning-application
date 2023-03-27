using System;
using english_learning_application.Models;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers.Api.Dto
{
	public class DisplayWordResponseDto
	{
		public DisplayWordResponseDto(DisplayWord displayWord)
		{
            ID = displayWord.ID;
            WordId = displayWord.WordId;
            Display = displayWord.Display;
        }

        public int ID { get; set; }

        //foreign key
        //many DisplayWord to one Word
        public int WordId { get; set; }

        //unic key
        public string Display { get; set; }
    }
}

