using System;
namespace english_learning_application.Controllers.Api.Dto
{
	public class DisplaySentenceRequestDto
	{
		public DisplaySentenceRequestDto()
        {
   
        }

        //foreign key
        //many DisplaySentence to one Sentence
        public int SentenceId { get; set; }

        //unic key
        public string Display { get; set; }
    }
}

