using System;
using System.ComponentModel.DataAnnotations;
using english_learning_application.Models;

namespace english_learning_application.Controllers.Api.Dto
{
	public class ContextRequestDto
	{
        [Required]
        public string Name { get; set; }

        [Required]
        public List<int> TranslatedWordIds { get; set; }

        [Required]
        public List<int> TranslatedSentenceIds { get; set; }

        [Required]
        public List<int> SentenceIds { get; set; }

        public ContextRequestDto() {
            TranslatedWordIds = new List<int>();
            TranslatedSentenceIds = new List<int>();
            SentenceIds = new List<int>();
        }
    }
}

