using System;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class DisplaySentence
	{
        public int ID { get; set; }

        //foreign key
        //many DisplaySentence to one Sentence
        public int SentenceId { get; set; }
        public Sentence Sentence { get; set; }

        //unic key
        public string Display { get; set; }
    }
}

