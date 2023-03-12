using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID))]
    public class TranslatedSentence
	{
        public int ID { get; set; }

        public List<Context> Contexts { get; set; }

        public int OwnerId { get; set; }

        public string Word { get; set; }

        public string TranslSentence { get; set; }

        public string TransLanguage { get; set; }
    }
}

