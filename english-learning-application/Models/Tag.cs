using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace english_learning_application.Models
{
    [PrimaryKey(nameof(ID), nameof(Name))]
    public class Tag
	{
		//public Tag()
		//{
  //      }

        public int ID { get; set; }

        public string Name { get; set; }

    }
}

