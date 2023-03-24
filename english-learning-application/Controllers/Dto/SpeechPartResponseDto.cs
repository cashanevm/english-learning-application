using System;
using english_learning_application.Models;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers.Dto
{
	public class SpeechPartResponseDto
	{
		public SpeechPartResponseDto(SpeechPart speechPart)
		{
            ID = speechPart.ID;
            Name = speechPart.Name;
            TranslatedWordIds = speechPart.TranslatedWords.Select(x => x.ID).ToList();
        }

        public int ID { get; set; }

        //unic key
        public string Name { get; set; }

        //one SpeechPart to many TranslatedWord
        public List<int> TranslatedWordIds { get; set; }
    }
}

