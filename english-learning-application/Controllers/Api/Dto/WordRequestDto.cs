using System;
using english_learning_application.Models;

namespace english_learning_application.Controllers.Api.Dto
{
	public class WordRequestDto
	{

        public WordRequestDto()
        {
            TagIds = new List<int>();
        }

        //many Word to many Tag
        public List<int> TagIds { get; set; }
        //unic key
        public string OriginalWord { get; set; }
    
	}
}

