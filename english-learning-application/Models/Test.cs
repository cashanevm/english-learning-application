using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class Test
	{

        public int ID { get; set; }

        public int TimePerWord { get; set; }

        public int OwnerId { get; set; }

        public int Options { get; set; }

        public List<Word> Words { get; set; }

        public string Language { get; set; }


    }
}

