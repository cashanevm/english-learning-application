using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID), nameof(Name))]
    public class Context
    {

        public int ID { get; set; }

        public string Name { get; set; }

    }
    
}

