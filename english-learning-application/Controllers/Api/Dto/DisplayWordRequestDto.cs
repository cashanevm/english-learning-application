using System;

namespace english_learning_application.Controllers.Api.Dto
{
    public class DisplayWordRequestDto
    {
        public DisplayWordRequestDto()
        {

        }

        //foreign key
        //many DisplayWord to one Word
        public int WordId { get; set; }

        //unic key
        public string Display { get; set; }
    }
}

