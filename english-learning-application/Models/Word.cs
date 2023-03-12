using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID), nameof(OriginalWord))]
    public class Word
	{
        public int ID { get; set; }

        public List<Tag> Tags { get; set; }

        public List<string> WordDisplay { get; set; }

        public string OriginalWord { get; set; }
    }
}

