using System;
using english_learning_application.Models;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Controllers.Dto
{
	public class TagResponseDto
    {
		public TagResponseDto(Tag tag)
		{
            ID = tag.ID;
            Name = tag.Name;
            WordIds = tag.Words.Select(x => x.ID).ToList();
        }

        public int ID { get; set; }

        //unic key
        public string Name { get; set; }

        //many Tag to many Word
        public List<int> WordIds { get; set; }
    }
}

