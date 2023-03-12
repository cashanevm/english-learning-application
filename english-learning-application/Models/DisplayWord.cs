using System;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class DisplayWord
	{
        public int ID { get; set; }

        //foreign key
        //many DisplayWord to one Word
        public int WordId { get; set; }
        public Word Word { get; set; }

        //unic key
        public string Display { get; set; }
    }
}

